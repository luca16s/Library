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
using Api.Properties;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class SwaggerExtensions
{
    /// <summary>
    /// Método de extensão para incluir configurações de swagger.
    /// </summary>
    /// <param name="services">
    /// <see cref="IServiceCollection"/>
    /// </param>
    /// <param name="swaggerInfo">
    /// Arquivo de modelo com informações de swagger.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Exceção caso propriedade de site não esteja preenchida.
    /// </exception>
    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services
    )
    {
        SwaggerInfo swaggerInfo = services
            .BuildServiceProvider()
            .GetService<IOptions<SwaggerInfo>>()?.Value
            ?? throw new NullReferenceException("Não foi possível recuperar as informações do swagger.");

        return services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                swaggerInfo.AppVersion,
                new OpenApiInfo
                {
                    Title = swaggerInfo.AppName,
                    Version = swaggerInfo.AppVersion,
                    Description = swaggerInfo.Description,
                    Contact = new OpenApiContact
                    {
                        Url = new(swaggerInfo.Site ?? string.Empty),
                        Name = swaggerInfo.Company,
                        Email = swaggerInfo.Email,
                    }
                }
            );
            c.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = Resources.SECURITY_SCHEME_HEADER_NAME,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = Resources.SECURITY_SCHEME_DESCRIPTION,
                }
            );
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
         {
                {
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Scheme = Resources.OAUTH_SCHEME,
                        BearerFormat = Resources.FORMAT,
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
}
