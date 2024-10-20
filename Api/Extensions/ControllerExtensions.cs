// -----------------------------------------------------------------------
// <copyright file="ControllerExtensions.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Extensions;

using Api.Conventions;
using Api.Helpers;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

/// <summary>
/// Classe de extensão de Controller.
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Adiciona o HttpContext Accessor.
    /// </summary>
    /// <param name="services">
    /// <see cref="IServiceCollection"/>
    /// </param>
    /// <see cref="IServiceCollection"/>
    /// </returns>
    public static IServiceCollection AddHttpContextAccessor
    (
        this IServiceCollection services
    ) => services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    /// <summary>
    /// Adiciona configuração de autorização.
    /// </summary>
    /// <param name="services">
    /// <see cref="IServiceCollection"/>
    /// </param>
    /// <returns>
    /// <see cref="IServiceCollection"/>
    /// </returns>
    public static IServiceCollection AddAuthorization
    (
        this IServiceCollection services
    ) => services.AddAuthorization(authOptions =>
    {
        authOptions.AddPolicy(
            JwtBearerDefaults.AuthenticationScheme,
            new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
            .RequireAuthenticatedUser()
            .Build()
        );
    });

    /// <summary>
    /// Adiciona configurações de controller.
    /// </summary>
    /// <param name="services">
    /// <see cref="IServiceCollection"/>
    /// </param>
    /// <returns>
    /// <see cref="IServiceCollection"/>
    /// </returns>
    public static IMvcBuilder AddController
    (
        this IServiceCollection services
    ) => services.AddControllers(config =>
    {
        AuthorizationPolicy? policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        config.Filters.Add(new AuthorizeFilter(policy));
        config.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
        config.Conventions.Add(new RouteTokenTransformerConvention(new URLTransformer()));
    }).AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
}
