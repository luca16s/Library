// -----------------------------------------------------------------------
// <copyright file="AuthenticateSchemeMiddleware.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Middlewares;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using System;
using System.Threading.Tasks;

/// <summary>
/// Middleware de autenticação.
/// </summary>
/// <remarks>
/// Cria um novo middleware para verificar se o usuário está autenticado na aplicação
/// </remarks>
/// <param name="next">Próximo middleware do pipeline</param>
/// <param name="scheme">Schema de autenticação</param>
public class AuthenticateSchemeMiddleware(RequestDelegate next, string scheme)
{
    private readonly RequestDelegate _next = next;
    private readonly string _scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));

    /// <summary>
    /// Executa o middleware de forma assíncrona
    /// </summary>
    /// <param name="httpContext">
    /// <see cref="HttpContext"/>
    /// Contexto HTTP do request
    /// </param>
    /// <returns>
    /// </returns>
    public async Task Invoke(HttpContext httpContext)
    {
        AuthenticateResult? result = await httpContext.AuthenticateAsync(_scheme);

        if (result != null
            && result.Succeeded
            && result.Principal != null)
        {
            httpContext.User = result.Principal;
        }

        await _next(httpContext);
    }
}
