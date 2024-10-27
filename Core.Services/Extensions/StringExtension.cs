// -----------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Services.Extensions;
using Core.Services.Exceptions;

using System.ComponentModel;
using System.Reflection;

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
    public static TEnum? GetEnumFromDescription<TEnum>(
        this string value
    ) where TEnum : Enum
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        foreach (FieldInfo field in typeof(TEnum).GetFields())
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute)
                if (descriptionAttribute.Description.Equals(value, StringComparison.Ordinal))
                    return (TEnum?)field.GetValue(value);

        throw new EnumItemNotFoundException(value);
    }

    /// <summary>
    /// Formata string utilizando string.Format.
    /// </summary>
    /// <param name="text">
    /// String a ser formatada.
    /// </param>
    /// <param name="extraTexts">
    /// Lista de strings extras.
    /// </param>
    /// <returns>
    /// String formatada.
    /// </returns>
    public static string FormatText(
        this string text,
        params string[] extraTexts
    )
    {
        if (extraTexts is null || extraTexts.Length == 0)
            return string.Format(text, string.Empty).TrimEnd();

        return string.Format(text, extraTexts);
    }
}
