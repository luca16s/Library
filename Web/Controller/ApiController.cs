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

    using Core.Interfaces.Services;
    using Core.Models;

    using Mediator.Interfaces;
    using Mediator.Notifications;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Controller base para API.
    /// </summary>
    /// <typeparam name="TService">
    /// Tipo do serviço a ser utilizado.
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// Tipo da entidade usada no serviço.
    /// </typeparam>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    [ApiController]
    [Produces("application/json")]
    public class ApiController<TService, TEntity, TId, TResponse> : ControllerBase
        where TId : struct
        where TResponse : notnull
        where TEntity : Entity<TId>
        where TService : IService<TEntity, TId>
    {
        private readonly IMapper _mapper;
        private readonly IDomainNotificationHandler<TId, TResponse> _notificationHandler;

        public readonly IMediatorHandler Mediator;
        public readonly IService<TEntity, TId> Service;

        /// <summary>
        /// Constrói uma nova instância da classe de api de controller.
        /// </summary>
        /// <param name="mapper">
        /// Injeção do automapper.
        /// </param>
        /// <param name="mediator">
        /// Injeção do mediator.
        /// </param>
        /// <param name="service">
        /// Injeção do serviço padrão da controller.
        /// </param>
        /// <param name="notificationHandler">
        /// Injeção do manipulador de Notificações.
        /// </param>
        public ApiController
        (
            IMapper mapper,
            IMediatorHandler mediator,
            IService<TEntity, TId> service,
            IDomainNotificationHandler<TId, TResponse> notificationHandler
        )
        {
            _mapper = mapper;
            Service = service;
            Mediator = mediator;
            _notificationHandler = notificationHandler;
        }

        [NonAction]
        protected bool IsOperationValid()
        {
            return _notificationHandler != null && !_notificationHandler.HasNotifications();
        }

        [NonAction]
        protected async Task NotifyError(string codigo, string mensagem)
        {
            await Mediator.RaiseEvent<DomainNotification<TId, TResponse>, TId, TResponse>(new DomainNotification<TId, TResponse>(codigo, mensagem));
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
        protected IActionResult GetResponse<Response, ViewModel>(Response response)
            where Response : notnull
            where ViewModel : notnull
        {
            return response is null ?
                NoContent() :
                IsOperationValid() ?
                Ok(_mapper.Map<ViewModel>(response)) :
                BadRequest(new { errors = _notificationHandler.GetNotifications().Select(p => p.Value) });
        }
    }
}
