// -----------------------------------------------------------------------
// <copyright file="ConfigurationBase.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Data;

using Core.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Classe de configuração base do banco de dados.
/// </summary>
/// <typeparam name="TEntity">
/// Entidade.
/// </typeparam>
public abstract class ConfigurationBase<TEntity> :
    IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// Configuração base da Entidade.
    /// </summary>
    /// <param name="builder">
    /// API para configuração da chamada do banco de dados.
    /// </param>
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        _ = builder.HasKey(u => u.Id);

        _ = builder.Ignore(p => p.ValidationResult);
    }
}
