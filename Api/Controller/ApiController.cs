// -----------------------------------------------------------------------
// <copyright file="ApiController.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Controller;

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
/// <typeparam name="TEntity">
/// Tipo da entidade usada no serviço.
/// </typeparam>
public class ApiController<TEntity>
    : ControllerBase
    where TEntity : Entity
{
    private readonly IDomainNotificationHandler handler;
    protected readonly IMediatorHandler mediator;

    /// <summary>
    /// Constrói uma nova instância da classe de api de controller.
    /// </summary>
    /// <param name="mediator">
    /// Injeção do mediator.
    /// </param>
    /// <param name="handler">
    /// Injeção do manipulador de Notificações.
    /// </param>
    public ApiController
    (
        IMediatorHandler mediator,
        IDomainNotificationHandler handler
    )
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(handler);

        this.handler = handler;
        this.mediator = mediator;
    }

    [NonAction]
    protected bool IsOperationValid() => handler is not null && !handler.HasNotifications();

    [NonAction]
    protected IActionResult GetResponse(
    )
    {
        List<string> errors = [.. handler.GetNotifications().Select(p => p.Value)];

        return IsOperationValid() ?
            Ok() :
            BadRequest(new { errors })
        ;
    }

    [NonAction]
    protected async Task NotifyError(
        string codigo,
        string mensagem
    )
    {
        await mediator.RaiseError(new ErrorNotification { StackTrace = mensagem, Exception = codigo });
    }

    [NonAction]
    protected async Task NotifyInvalidErrorModelAsync(
        string typeName
    )
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
    protected IActionResult GetResponse<Response, ViewModel>(
        IMapper mapper,
        Response response
    )
        where Response : notnull
        where ViewModel : notnull
    {
        List<string> errors = [.. handler.GetNotifications().Select(p => p.Value)];

        return response is null ?
            NoContent() :
            IsOperationValid() ?
            Ok(mapper.Map<ViewModel>(response)) :
            BadRequest(new { errors });
    }
}

/// <summary>
/// Controller base para API.
/// </summary>
/// <typeparam name="TEntity">
/// Tipo da entidade usada no serviço.
/// </typeparam>
/// <typeparam name="TService">
/// Tipo do serviço a ser utilizado.
/// </typeparam>
[ApiController]
[Produces("application/json")]
public class ApiController<TEntity, TService>
    : ControllerBase
    where TEntity : Entity
    where TService : IService<TEntity>
{
    private readonly IDomainNotificationHandler handler;
    protected readonly TService service;
    protected readonly IMediatorHandler mediator;

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
    /// <param name="handler">
    /// Injeção do manipulador de Notificações com resposta.
    /// </param>
    /// <param name="handlerWithOutResponse">
    /// Injeção do manipulador de Notificações sem resposta.
    /// </param>
    public ApiController
    (
        TService service,
        IMediatorHandler mediator,
        IDomainNotificationHandler handler
    )
    {
        if (service is null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        ArgumentNullException.ThrowIfNull(handler);
        ArgumentNullException.ThrowIfNull(mediator);

        this.service = service;
        this.handler = handler;
        this.mediator = mediator;
    }

    [NonAction]
    protected bool IsOperationValid() => handler is not null && !handler.HasNotifications();

    [NonAction]
    protected IActionResult GetResponse(
    )
    {
        List<string> errors = [.. handler.GetNotifications().Select(p => p.Value)];

        return IsOperationValid() ?
            Ok() :
            BadRequest(new { errors })
        ;
    }

    [NonAction]
    protected async Task NotifyError(
        string codigo,
        string mensagem
    )
    {
        await mediator.RaiseError(new ErrorNotification { StackTrace = mensagem, Exception = codigo });
    }

    [NonAction]
    protected async Task NotifyInvalidErrorModelAsync(
        string typeName
    )
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
    protected IActionResult GetResponse<Response, ViewModel>(
        IMapper mapper,
        Response response
    )
        where Response : notnull
        where ViewModel : notnull
    {
        List<string> errors = [.. handler.GetNotifications().Select(p => p.Value)];

        return response is null ?
            NoContent() :
            IsOperationValid() ?
            Ok(mapper.Map<ViewModel>(response)) :
            BadRequest(new { errors });
    }
}
