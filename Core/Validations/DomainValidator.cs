// -----------------------------------------------------------------------
// <copyright file="DomainValidator.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Validations
{
    using Core.Models;

    using FluentValidation;

    /// <summary>
    /// Classe de validação de domínio.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Entidade a ser validada.
    /// </typeparam>
    /// <typeparam name="TType">
    /// Tipo do identificador da entidade.
    /// </typeparam>
    public abstract class DomainValidator<TEntity, TType> : AbstractValidator<TEntity>
        where TEntity : Entity<TType>
        where TType : struct
    {
        protected readonly TEntity _entidade;

        protected DomainValidator(TEntity entidade)
        {
            if (entidade is null) throw new ArgumentNullException(nameof(entidade), "Entidade não pode ser nula.");

            _entidade = entidade;
        }

        /// <summary>
        /// Valida Entidade de domínio.
        /// </summary>
        protected abstract void Validar();
    }
}
