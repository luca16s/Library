// -----------------------------------------------------------------------
// <copyright file="ApiController.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Web.Controller
{
    using AutoMapper;

    using CQRS.Commands;
    using CQRS.Handlers;
    using CQRS.Interfaces;
    using CQRS.Notifications;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Controller base para API.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    [ApiController]
    [Produces("application/json")]
    public class ApiController<TId> : ControllerBase where TId : struct
    {
        protected readonly IMapper _mapper;
        protected readonly IMediatorHandler _mediator;
        protected readonly DomainNotificationHandler<TId> _notifications;

        /// <summary>
        /// Constrói uma nova instância da classe de api de controller.
        /// </summary>
        /// <param name="mapper">
        /// Injeção do automapper.
        /// </param>
        /// <param name="mediator">
        /// Injeção do mediator.
        /// </param>
        /// <param name="notifications">
        /// Injeção do manipulador de Notificações.
        /// </param>
        public ApiController
        (
            IMapper mapper,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification<TId>> notifications
        )
        {
            _mapper = mapper;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler<TId>)notifications;
        }

        [NonAction]
        protected new async Task<IActionResult> Response<TCommand>(TCommand command)
            where TCommand : Command<TId>
        {
            if (!ModelState.IsValid)
            {
                await NotifyInvalidErrorModelAsync(command?.GetType()?.Name ?? string.Empty);
            }

            return OperacaoValida() ?
                Ok(command?.Result) :
                BadRequest(
                    new
                    {
                        errors = _notifications.GetNotifications().Select(p => p.Value)
                    }
                );
        }

        [NonAction]
        protected bool OperacaoValida()
        {
            return !_notifications.HasNotifications();
        }

        [NonAction]
        protected async Task NotifyInvalidErrorModelAsync(string typeName)
        {
            IEnumerable<ModelError>? erros = ModelState.Values.SelectMany(m => m.Errors) ?? new List<ModelError>();

            foreach (var erro in erros)
            {
                string? erroMsg = erro.Exception == null ?
                    erro.ErrorMessage :
                    erro.Exception.Message;

                await NotifyError(typeName, erroMsg);
            }
        }

        [NonAction]
        protected async Task NotifyError(string codigo, string mensagem)
        {
            await _mediator.RaiseEvent<DomainNotification<TId>, TId>(new DomainNotification<TId>(codigo, mensagem));
        }
    }
}
