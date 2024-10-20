// -----------------------------------------------------------------------
// <copyright file="ErrorHandlingMiddleware.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Middlewares;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using System;
using System.Net;
using System.Threading.Tasks;

/// <summary>
/// Middleware de erro.
/// </summary>
/// <remarks>
/// Inicializa uma nova instância do middleware de erro.
/// </remarks>
/// <param name="next">
/// <see cref="RequestDelegate"/>
/// </param>
public class ErrorHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    /// <summary>
    /// Invocação do contexto de erro.
    /// </summary>
    /// <param name="context">
    /// <see cref="HttpContext"/>
    /// </param>
    /// <returns>
    /// </returns>
    public async Task Invoke(
        HttpContext context
    )
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Gerencia o contexto de exceções.
    /// </summary>
    /// <param name="context">
    /// <see cref="HttpContext"/>
    /// </param>
    /// <param name="exception">
    /// <see cref="Exception"/>
    /// </param>
    /// <returns></returns>
    private static Task HandleExceptionAsync(
        HttpContext context,
        Exception exception
    )
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
    }
}
