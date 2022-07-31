// -----------------------------------------------------------------------
// <copyright file="DomainSpecification.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Validations
{
    using Core.Models;

    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Classe de especificação de domínio.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Entidade.
    /// </typeparam>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    [ExcludeFromCodeCoverage]
    public abstract class DomainSpecification<TEntity, TId>
        where TId : struct
        where TEntity : Entity<TId>
    {
        protected readonly TEntity _entidade;

        protected DomainSpecification(TEntity entidade)
        {
            if (entidade is null) throw new ArgumentNullException(nameof(entidade), "Entidade não pode ser nula.");

            _entidade = entidade;
        }

        /// <summary>
        /// Verifica se entidade é válida.
        /// </summary>
        /// <returns>
        /// True: Entidade é válida.
        /// False: Entidade é inválida.
        /// </returns>
        public abstract bool IsValid();
    }
}
