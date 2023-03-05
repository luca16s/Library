namespace Web.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    public static class ControllerExtensions
    {
        public static void AddControllerConfiguration
        (
            this IServiceCollection services
        )
        {
            _ = services.AddAuthorization(authOptions =>
            {
                authOptions.AddPolicy(
                    JwtBearerDefaults.AuthenticationScheme,
                    new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build()
                );
            });

            _ = services.AddControllers(config =>
            {
                AuthorizationPolicy? policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));

            }).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            _ = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
