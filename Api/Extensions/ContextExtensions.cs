// -----------------------------------------------------------------------
// <copyright file="ContextExtensions.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Extensions;

using Api.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ContextExtensions
{
    public static IServiceCollection AddContextConfiguration<TContext>
    (
        this IServiceCollection services,
        string schema,
        Settings settings
    ) where TContext : DbContext
    {
        string? connectionString =
            settings
            .ConnectionStrings
            .FirstOrDefault(c => c.Nome.Equals(schema, StringComparison.Ordinal))
           ?.Url;

        return string.IsNullOrWhiteSpace(connectionString)
            ? throw new InvalidOperationException(
                "String de conexão com o banco de dados não pode ser nula."
            )
            : services
            .AddEntityFrameworkProxies()
            .AddDbContext<TContext>(options =>
            {
                _ = options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
            });
    }
}
