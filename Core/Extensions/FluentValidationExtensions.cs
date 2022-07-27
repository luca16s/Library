// -----------------------------------------------------------------------
// <copyright file="FluentValidationExtensions.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Extensions
{
    using Core.Models;
    using Core.Validations;

    using FluentValidation;

    /// <summary>
    /// Classe de extensão para operações com Classes de validação.
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Verifica se entidade é válida.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Entidade a ser validada.
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// Propriedade a ser validada.
        /// </typeparam>
        /// <typeparam name="TType">
        /// Tipo da propriedade.
        /// </typeparam>
        /// <param name="preMadeRules">
        /// Regras pré-definidas a serem executadas.
        /// </param>
        /// <param name="predicate">
        /// Func com as propriedades a serem validadas.
        /// </param>
        /// <returns>
        /// Retorna o RuleBuilder da Entidade passada.
        /// </returns>
        public static IRuleBuilderOptions<TEntity, TProperty> IsValid<TEntity, TProperty, TType>(
            this IRuleBuilder<TEntity,
            TProperty> preMadeRules,
            DomainSpecification<TEntity, TType> predicate)
            where TEntity : Entity<TType>
            where TType : struct
        {
            return preMadeRules.Must(p => predicate.IsValid());
        }
    }
}
