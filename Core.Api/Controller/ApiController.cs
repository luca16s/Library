// -----------------------------------------------------------------------
// <copyright file="ApiController.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Api.Controller;

using AutoMapper;

using Core.Interfaces;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller base para API.
/// </summary>
/// <typeparam name="TEntity">
/// Tipo da entidade usada no serviço.
/// </typeparam>
[ApiController]
[Produces("application/json")]
public class ApiController<TId, TEntity> : ControllerBase
    where TId : notnull
    where TEntity : IEntity<TId>
{
    [NonAction]
    protected IActionResult GetResponse<Response, ViewModel>(
        IMapper mapper,
        Response response
    )
        where Response : notnull
        where ViewModel : notnull
    {
        return response is null ?
            NoContent() :
            Ok(mapper.Map<ViewModel>(response));
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
public class ApiController<TId, TEntity, TService> : ControllerBase
    where TId : notnull
    where TEntity : IEntity<TId>
    where TService : IService<TId, TEntity>
{
    protected readonly TService service;

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
        TService service
    )
    {
        ArgumentNullException.ThrowIfNull(service);

        this.service = service;
    }

    [NonAction]
    protected IActionResult GetResponse<Response, ViewModel>(
        IMapper mapper,
        Response response
    )
        where Response : notnull
        where ViewModel : notnull
    {
        return response is null ?
            NoContent() :
            Ok(mapper.Map<ViewModel>(response));
    }

    [NonAction]
    protected async Task<ValidationResult> GetValidationResultAsync<TDto>(
        TDto dto,
        IValidator<TDto> validator
    ) => await validator.ValidateAsync(dto);

    [NonAction]
    protected TEntity MapToDataModel<TDto>(
        TDto dto,
        IMapper mapper
    ) => mapper.Map<TEntity>(dto);

    [NonAction]
    protected TDto MapToDto<TDto>(
        TEntity dto,
        IMapper mapper
    ) => mapper.Map<TDto>(dto);
}
