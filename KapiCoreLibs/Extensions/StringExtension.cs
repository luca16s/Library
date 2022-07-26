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
        /// <typeparam name="T">
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
        public static T? GetEnumValueFromDescription<T>(this string value) where T : Enum
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    is DescriptionAttribute descriptionAttribute)
                {
                    if (descriptionAttribute.Description.Equals(value, StringComparison.Ordinal))
                    {
                        return (T?)field.GetValue(value);
                    }
                }
            }

            throw new EnumItemNotFoundException(value);
        }
    }
}
