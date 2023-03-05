namespace Web.Middlewares
{
    using Microsoft.AspNetCore.Http;

    using Newtonsoft.Json;

    using System;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Middleware de erro.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Inicializa uma nova instância do middleware de erro.
        /// </summary>
        /// <param name="next">
        /// <see cref="RequestDelegate"/>
        /// </param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invocação do contexto de erro.
        /// </summary>
        /// <param name="context">
        /// <see cref="HttpContext"/>
        /// </param>
        /// <returns>
        /// </returns>
        public async Task Invoke(HttpContext context)
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
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
        }
    }
}
