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

    /// <summary>
    /// Classe de especificação de domínio.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Entidade.
    /// </typeparam>
    /// <typeparam name="TType">
    /// Tipo do identificador da entidade.
    /// </typeparam>
    public abstract class DomainSpecification<TEntity, TType>
        where TEntity : Entity<TType>
        where TType : struct
    {
        protected readonly TEntity _entidade;

        protected DomainSpecification(TEntity entidade)
        {
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
