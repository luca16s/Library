// -----------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Core.Exceptions;

using System.ComponentModel;
using System.Reflection;

namespace Core.Extensions
{
    /// <summary>
    /// Classe de extensão para operações com string.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Busca o valor de um enum através de uma string.
        /// </summary>
        /// <typeparam name="TEnum">
        /// Tipo do enum.
        /// </typeparam>
        /// <param name="value">
        /// Texto do enum.
        /// </param>
        /// <returns>
        /// Retorna item do enum.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Item não encontrado.
        /// </exception>
        public static TEnum? GetEnumValueFromDescription<TEnum>(this string value) where TEnum : Enum
        {
            foreach (FieldInfo field in typeof(TEnum).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    is DescriptionAttribute descriptionAttribute)
                {
                    if (descriptionAttribute.Description.Equals(value, StringComparison.Ordinal))
                    {
                        return (TEnum?)field.GetValue(value);
                    }
                }
            }

            throw new EnumItemNotFoundException(value);
        }

        /// <summary>
        /// Formata mensagem de erro.
        /// </summary>
        /// <param name="message">
        /// Mensagem a ser passada.
        /// </param>
        /// <param name="property">
        /// Lista de propriedades.
        /// </param>
        /// <returns>
        /// Mensagem formatada.
        /// </returns>
        public static string FormatMessage(this string message, params string[] property)
        {
            return string.Format(message, property);
        }
    }
}
