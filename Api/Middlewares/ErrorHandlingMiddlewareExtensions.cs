// -----------------------------------------------------------------------
// <copyright file="ErrorHandlingMiddlewareExtensions.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Middlewares;

using Microsoft.AspNetCore.Builder;

/// <summary>
/// Classe de extensão do middleware de erro.
/// </summary>
public static class ErrorHandlingMiddlewareExtensions
{
    /// <summary>
    /// Método de extensão para uso de middleware de erro
    /// </summary>
    /// <param name="app">
    /// Aplication Builder
    /// </param>
    /// <returns>
    /// <see cref="IApplicationBuilder"/>
    /// </returns>
    public static IApplicationBuilder UseErrorHandling(
        this IApplicationBuilder app
    )
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
