namespace Web.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Web.Extensions.Models;
    using Web.Properties;

    public static class SwaggerExtensions
    {
        /// <summary>
        /// Método de extensão para incluir configurações de swagger.
        /// </summary>
        /// <param name="services">
        /// <see cref="IServiceCollection"/>
        /// </param>
        /// <param name="swaggerModel">
        /// Arquivo de modelo com informações de swagger.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Exceção caso propriedade de site não esteja preenchida.
        /// </exception>
        public static void AddSwaggerConfiguration(
            this IServiceCollection services,
            SwaggerModel swaggerModel
        )
        {
            if (swaggerModel is null) throw new ArgumentNullException(nameof(swaggerModel));

            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    swaggerModel.Version,
                    new OpenApiInfo
                    {
                        Title = swaggerModel.AppName,
                        Version = swaggerModel.Version,
                        Description = swaggerModel.Description,
                        Contact = new OpenApiContact
                        {
                            Url = new(swaggerModel.Site),
                            Name = swaggerModel.Empresa,
                            Email = swaggerModel.Email,
                        }
                    }
                );
                c.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = Resources.SECURITY_SCHEME_HEADER_NAME,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Description = Resources.SECURITY_SCHEME_DESCRIPTION,
                    }
                );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
             {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            },
                            In = ParameterLocation.Header,
                            Scheme = Resources.OAUTH_SCHEME,
                            BearerFormat = Resources.FORMAT,
                        },
                        new List<string>()
                    }
             });
                c.ResolveConflictingActions(apiDescription => apiDescription.First());
                c.EnableAnnotations();
                c.OrderActionsBy(
                    (apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}"
                );
                c.DescribeAllParametersInCamelCase();
            });
        }
    }
}
