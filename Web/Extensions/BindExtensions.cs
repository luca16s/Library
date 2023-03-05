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

    /// <summary>
    /// Classe de extensão de Bind.
    /// </summary>
    public static class BindExtensions
    {
        /// <summary>
        /// Adiciona configuração do arquivo de Settings.
        /// </summary>
        /// <param name="services">
        /// <see cref="IServiceCollection"/>
        /// </param>
        /// <param name="configuration">
        /// <see cref="IConfiguration"/>
        /// </param>
        public static void AddSettings
        (
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            _ = services.Configure<Settings>(configuration.GetSection(nameof(Settings)));
            _ = services.AddSingleton<Settings>();
        }

        /// <summary>
        /// Adiciona configuração do MediatR.
        /// </summary>
        /// <param name="services">
        /// <see cref="IServiceCollection"/>
        /// </param>
        /// <param name="assembly">
        /// Assembly contendo as configurações de MediatR
        /// <see cref="Assembly"/>
        /// </param>
        public static void AddMediatR
        (
           this IServiceCollection services,
           Assembly assembly
        ) => _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        /// <summary>
        /// Adiciona configuração do AutoMapper.
        /// </summary>
        /// <param name="services">
        /// <see cref="IServiceCollection"/>
        /// <param name="assembly">
        /// Assembly contendo as configurações de MediatR
        /// <see cref="Assembly"/>
        /// </param>
        public static void AddAutomapper
        (
            this IServiceCollection services,
           Assembly assembly
        ) => _ = services.AddAutoMapper(cfg => cfg.AddMaps(assembly), assembly);

        /// <summary>
        /// Adiciona configuração de CORS.
        /// </summary>
        /// <param name="services">
        /// <see cref="IServiceCollection"/>
        /// </param>
        /// <param name="settings">
        /// <see cref="Settings"/> Arquivo de configurações.
        /// </param>
        /// <param name="corsPolicyName">
        /// Nome da política de CORS.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Exceção caso argumento Settings seja nulo.
        /// </exception>
        public static void AddCors
        (
            this IServiceCollection services,
            Settings settings,
            string corsPolicyName
        )
        {
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));

            _ = services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName, builder =>
                {
                    foreach (string domain in settings.AllowedDomains)
                    {
                        _ = builder.WithOrigins(domain)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                });
            });
        }

        /// <summary>
        /// Adiciona configuração de JWT.
        /// </summary>
        /// <param name="services">
        /// <see cref="IServiceCollection"/>
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Exceção caso classe Settings não esteja preenchida.
        /// </exception>
        public static void AddJwt
        (
           this IServiceCollection services
        )
        {
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
        }
    }
}
