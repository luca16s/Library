// -----------------------------------------------------------------------
// <copyright file="BindExtensions.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Extensions;

using Api.Models;

using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Reflection;

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
    /// <param name="nomes">
    /// Nomes das configurações a serem adicionadas.
    /// </param>
    public static void AddSettingsConfiguration
    (
        this IServiceCollection services,
        IConfiguration configuration,
        params string[] nomes
    )
    {
        foreach (var nome in nomes)
        {
            _ = services.Configure<Settings>(configuration.GetSection(nome));
        }
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
    public static IServiceCollection AddMediatRConfiguration
    (
       this IServiceCollection services,
       Assembly assembly
    ) => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

    /// <summary>
    /// Adiciona configuração do AutoMapper.
    /// </summary>
    /// <param name="services">
    /// <see cref="IServiceCollection"/>
    /// <param name="assembly">
    /// Assembly contendo as configurações de MediatR
    /// <see cref="Assembly"/>
    /// </param>
    public static void AddAutoMapperConfiguration
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
    public static IServiceCollection AddCorsConfiguration
    (
        this IServiceCollection services,
        Settings settings,
        string corsPolicyName
    )
    {
        return settings is null
            ? throw new ArgumentNullException(nameof(settings))
            : services.AddCors(options =>
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
    public static void AddJwtConfiguration
    (
       this IServiceCollection services,
       JwtSettings tokenSettings,
       SigningSettings signingSettings
    )
    {
        if (tokenSettings is null)
        {
            throw new NullReferenceException(nameof(tokenSettings));
        }

        if (signingSettings is null)
        {
            throw new NullReferenceException(nameof(signingSettings));
        }

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

    /// <summary>
    /// Adiciona injeção de dependência de validadores de entidades.
    /// <param name="services">
    /// <see cref="IServiceCollection"/>
    /// <param name="assembly">
    /// Assembly contendo as configurações de MediatR
    /// <see cref="Assembly"/>
    /// </param>
    public static void AddFluentValidation
    (
        this IServiceCollection services,
       Assembly assembly
    ) => _ = services.AddValidatorsFromAssembly(assembly);
}
