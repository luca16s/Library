// -----------------------------------------------------------------------
// <copyright file="ConfigurationBase.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Data
{
    using Core.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Classe de configuração base do banco de dados.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Entidade.
    /// </typeparam>
    /// <typeparam name="TType">
    /// Tipo do identificador da Entidade.
    /// </typeparam>
    public abstract class ConfigurationBase<TEntity, TType> :
        IEntityTypeConfiguration<TEntity>
        where TEntity : Entity<TType>
        where TType : struct
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

            _ = builder.Ignore(c => c.ValidationResult);
        }
    }
}
