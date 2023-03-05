namespace Web.Middlewares
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
