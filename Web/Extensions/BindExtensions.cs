namespace Web.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using System;
    using System.Reflection;

    using Web.Models;

    public static class BindExtensions
    {
        public static IServiceCollection AddSettingsConfiguration
        (
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            return services.Configure<Settings>(
                configuration.GetSection(nameof(Settings))
            );
        }

        public static IServiceCollection AddMediatRConfiguration
        (
           this IServiceCollection services,
           Assembly type
        )
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(type));
        }

        public static IServiceCollection AddAutomapper
        (
            this IServiceCollection services,
            Type type
        )
        {
            return services.AddAutoMapper(
                cfg => cfg.AddMaps(type),
                type.GetTypeInfo().Assembly
            );
        }

        public static IServiceCollection AddCorsConfiguration
        (
            this IServiceCollection services,
            string corsPolicyName
        )
        {
            ServiceProvider? provider = services.BuildServiceProvider();
            var settings = provider.GetService<IOptions<Settings>>()?.Value;

            return settings is null
                   ? throw new NullReferenceException(nameof(settings))
                   : services.AddCors(options => {
                       options.AddPolicy(corsPolicyName, builder => {
                           foreach (string domain in settings.AllowedDomains) {
                               _ = builder.WithOrigins(domain)
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                           }
                       });
                   });
        }

        public static IServiceCollection AddJwtConfiguration
        (
           this IServiceCollection services
        )
        {
            _ = services.AddSingleton<Settings>();

            var provider = services.BuildServiceProvider();
            var tokenSettings = provider.GetService<JwtSettings>();
            var signingSettings = provider.GetService<SigningSettings>();

            if (tokenSettings is null)
                throw new NullReferenceException(nameof(tokenSettings));

            if (signingSettings is null)
                throw new NullReferenceException(nameof(signingSettings));

            _ = services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenSettings.Issuer,
                    ValidAudience = tokenSettings.Issuer,
                    IssuerSigningKey = signingSettings.SigningCredentials.Key,
                };
            });

            return services;
        }
    }
}
