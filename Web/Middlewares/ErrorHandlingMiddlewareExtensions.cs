namespace Web.Middlewares
{
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
}
