// -----------------------------------------------------------------------
// <copyright file="AuthenticateMiddlewareExtensions.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Classe de extensão para validação de autenticação do usuário
    /// </summary>
    public static class AuthenticateMiddlewareExtensions
    {
        /// <summary>
        /// Usa o middleware para verificar se o usuário está autenticado na aplicação
        /// </summary>
        /// <param name="app">Builder de pipeline da aplicação</param>
        /// <param name="scheme">Schema de autenticação</param>
        /// <returns>Builder de pipeline da aplicação</returns>
        public static IApplicationBuilder UseAuthenticationScheme(this IApplicationBuilder app, string scheme)
        {
            return app.UseMiddleware<AuthenticateSchemeMiddleware>(scheme);
        }
    }
}
