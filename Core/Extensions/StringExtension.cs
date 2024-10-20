// -----------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Extensions;
using Core.Exceptions;

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
    /// Formata mensagem utilizando string.Format.
    /// </summary>
    /// <param name="message">
    /// Mensagem a ser passada.
    /// </param>
    /// <param name="extraMessages">
    /// Lista de mensagens extras.
    /// </param>
    /// <returns>
    /// Mensagem formatada.
    /// </returns>
    public static string FormatMessage(
        this string message,
        params string[] extraMessages
    )
    {
        if (extraMessages is null || extraMessages.Length == 0)
            return string.Format(message, string.Empty).TrimEnd();

        return string.Format(message, extraMessages);
    }
}
