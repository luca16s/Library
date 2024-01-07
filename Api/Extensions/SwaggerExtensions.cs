// -----------------------------------------------------------------------
// <copyright file="SwaggerExtensions.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Extensions;

using Api.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
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
    )
    {
        return services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                settings.Swagger.AppVersion,
                new OpenApiInfo
                {
                    Title = settings.Swagger.AppName,
                    Version = settings.Swagger.AppVersion,
                    Description = settings.Swagger.Description,
                    Contact = new OpenApiContact
                    {
                        Url = new(settings.Swagger.Site ?? string.Empty),
                        Name = settings.Swagger.Company,
                        Email = settings.Swagger.Email,
                    }
                }
            );
            c.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = settings.Swagger.SecuritySchemeHeaderName,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = settings.Swagger.SecuritySchemeDescription,
                }
            );
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
            c.ResolveConflictingActions(apiDescription => apiDescription.First());
            c.EnableAnnotations();
            c.OrderActionsBy(
                (apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}"
            );
            c.CustomOperationIds(apiDesc =>
            {
                return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
            });
            c.DescribeAllParametersInCamelCase();
        });
    }

    public static void Swagger(
        this WebApplication app,
        Settings settings
    )
    {
        _ = app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{settings.Swagger.AppVersion}/swagger.json", settings.Swagger.AppName);
        });
    }
}
