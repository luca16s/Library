namespace Web.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Web.Properties;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(
            this IServiceCollection services,
            string? site = "",
            string? email = "",
            string? appName = "",
            string? version = "",
            string? empresa = "",
            string? description = ""
        )
        {
            return string.IsNullOrWhiteSpace(site) ?
                throw new ArgumentNullException(nameof(site)) :
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(
                        version,
                        new OpenApiInfo
                    {
                        Title = appName,
                        Version = version,
                        Description = description,
                        Contact = new OpenApiContact
                        {
                            Url = new(site),
                            Name = empresa,
                            Email = email,
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
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme,
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
