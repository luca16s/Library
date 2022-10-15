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

    using CrossCutting.ViewModels;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Controller base para API.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    [ApiController]
    [Produces("application/json")]
    public class ApiController<TId, TResponse> : ControllerBase
        where TId : struct
        where TResponse : notnull
    {
        protected readonly IMapper _mapper;
        protected readonly IMediatorHandler _mediator;
        protected readonly DomainNotificationHandler<TId, TResponse> _notifications;

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
            INotificationHandler<DomainNotification<TId, TResponse>> notifications
        )
        {
            _mapper = mapper;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler<TId, TResponse>)notifications;
        }

        [NonAction]
        protected bool OperacaoValida()
        {
            return !_notifications.HasNotifications();
        }

        [NonAction]
        protected async Task NotifyError(string codigo, string mensagem)
        {
            await _mediator.RaiseEvent<DomainNotification<TId, TResponse>, TId, TResponse>(new DomainNotification<TId, TResponse>(codigo, mensagem));
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
        protected new IActionResult Response<Response, ViewModel>(Response response)
            where Response : notnull
            where ViewModel : notnull
        {
            return OperacaoValida() ?
                Ok(
                    _mapper.Map<ViewModel>(response)
                ) :
                BadRequest(
                    new { errors = _notifications.GetNotifications().Select(p => p.Value) }
                );
        }
    }
}
