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
        private readonly IDomainNotificationHandler<TId> _handlerWithOutResponse;
        private readonly IDomainNotificationHandler<TId, TResponse> _handlerWithResponse;

        public readonly TService Service;
        public readonly IMediatorHandler Mediator;

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
        /// <param name="handlerWithResponse">
        /// Injeção do manipulador de Notificações com resposta.
        /// </param>
        /// <param name="handlerWithOutResponse">
        /// Injeção do manipulador de Notificações sem resposta.
        /// </param>
        public ApiController
        (
            IMapper mapper,
            TService service,
            IMediatorHandler mediator,
            IDomainNotificationHandler<TId> handlerWithOutResponse,
            IDomainNotificationHandler<TId, TResponse> handlerWithResponse
        )
        {
            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            if (service is null)
                throw new ArgumentNullException(nameof(service));

            if (mediator is null)
                throw new ArgumentNullException(nameof(mediator));

            if (handlerWithResponse is null)
                throw new ArgumentNullException(nameof(handlerWithResponse));

            if (handlerWithOutResponse is null)
                throw new ArgumentNullException(nameof(handlerWithOutResponse));

            _mapper = mapper;
            Service = service;
            Mediator = mediator;
            _handlerWithResponse = handlerWithResponse;
            _handlerWithOutResponse = handlerWithOutResponse;
        }

        [NonAction]
        protected bool IsOperationValid()
        {
            var withResponse = _handlerWithResponse != null && !_handlerWithResponse.HasNotifications();
            var withOutResponse = _handlerWithOutResponse != null && !_handlerWithOutResponse.HasNotifications();

            return withResponse && withOutResponse;
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
                BadRequest(new { errors = _handlerWithResponse.GetNotifications().Select(p => p.Value) });
        }
    }
}
