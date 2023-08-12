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
    using FluentValidation;

    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Classe de extensão para operações com Classes de validação.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Método de extensão que valida se texto contém somente dígitos.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo da entidade a ser validada.
        /// </typeparam>
        /// <param name="ruleBuilder">
        /// RuleBuilder para realizar validação.
        /// </param>
        /// <returns>
        /// Retorna RuleBuilder pós validação.
        /// </returns>
        public static IRuleBuilderOptions<TEntity, string> ShouldOnlyHaveDigits<TEntity>(
            this IRuleBuilder<TEntity, string> ruleBuilder
        )
        {
            return ruleBuilder.Must(property =>
            {
                return !string.IsNullOrWhiteSpace(property) &&
                property is string value &&
                value.All(c => c is >= '0' and <= '9');
            });
        }
    }
}