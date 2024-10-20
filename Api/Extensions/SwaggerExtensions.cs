// -----------------------------------------------------------------------
// <copyright file="SwaggerExtensions.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Extensions;

using Api.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Extensão de swagger.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Método de extensão para incluir configurações de swagger.
    /// </summary>
    /// <param name="services">
    /// <see cref="IServiceCollection"/>
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Exceção caso propriedade de site não esteja preenchida.
    /// </exception>
    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services,
        Settings settings
    ) => services.AddSwaggerGen(c =>
    {
        foreach (var version in settings.Swagger.SwaggerVersions)
            c.SwaggerDoc(
                $"v{version}",
                new OpenApiInfo
                {
                    Title = settings.Swagger.AppName,
                    Version = settings.Swagger.Version,
                    Description = settings.Swagger.Description,
                    Contact = new OpenApiContact
                    {
                        Email = settings.Swagger.Email,
                        Name = settings.Swagger.Company,
                        Url = new(settings.Swagger.Site ?? string.Empty),
                    },
                }
            );
        c.OrderActionsBy(
            (apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}"
        );
        c.EnableAnnotations();
        c.IgnoreObsoleteActions();
        c.AddSecurityDefinition(
            JwtBearerDefaults.AuthenticationScheme,
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Name = settings.Swagger.SecuritySchemeHeaderName,
                Description = settings.Swagger.SecuritySchemeDescription,
            }
        );
        c.IgnoreObsoleteProperties();
        c.CustomOperationIds(apiDesc =>
        {
            return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
        });
        c.DescribeAllParametersInCamelCase();
        c.DocInclusionPredicate((docName, apiDesc) =>
        {
            if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

            var declaringType = methodInfo.DeclaringType;

            if (declaringType is null) return false;

            var versions = declaringType
                .GetCustomAttributes(true)
                .OfType<ApiExplorerSettingsAttribute>()
                .Select(attr => attr?.GroupName ?? "")
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .ToList();

            return versions.Any(v => v == docName);
        });
        c.ResolveConflictingActions(apiDescription => apiDescription.First());
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Scheme = settings.Swagger.OAuthScheme,
                    BearerFormat = settings.Swagger.BearerFormat,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme,
                    },
                },
                new List<string>()
            }
        });
    });

    public static void Swagger(
        this WebApplication app,
        Settings settings
    )
    {
        _ = app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.UseRequestInterceptor(
                    """
                        (request) => {
                            if (!request.headers.Authorization)
                                request.headers.Authorization = 'Bearer ' + localStorage.getItem('jwt_token');

                            return request;
                        }
                    """.ReplaceLineEndings(string.Empty)
                );
                c.UseResponseInterceptor(
                    """
                        (response) => {
                            if (response.url.includes('/api/auth/login')) 
                                localStorage.setItem('jwt_token', response.obj.token);

                            if (response.url.includes('/api/auth/logout')) 
                                localStorage.removeItem('jwt_token');

                            return response;
                        }
                    """.ReplaceLineEndings(string.Empty)
                );
                c.DocumentTitle = settings.Swagger.AppName;
                foreach (var version in settings.Swagger.SwaggerVersions)
                    c.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"v{version}");
            });
    }
}
