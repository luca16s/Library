// -----------------------------------------------------------------------
// <copyright file="EnumExtension.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Services.Extensions;
using Core.Models;
using Core.Services.Exceptions;

using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Classe de extensão para operações com enumeradores.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Busca a descrição do Enumerador passado.
    /// </summary>
    /// <param name="value">
    /// Enum a ter a descrição retornada.
    /// </param>
    /// <returns>
    /// Retorna a descrição do enumerador em formato texto.
    /// </returns>
    /// <exception cref="EnumDescriptionNotFoundException">
    /// Descrição não encontrada.
    /// </exception>
    public static string Description(
        this Enum? value
    )
    {
        ArgumentNullException.ThrowIfNull(value);

        FieldInfo field = value.GetType().GetField($"{value}")
            ?? throw new ArgumentNullException(nameof(value), "Valor do enum não pode ser nulo.");

        DescriptionAttribute attribute = field.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false
        ).FirstOrDefault() as DescriptionAttribute
            ?? throw new EnumDescriptionNotFoundException();

        return string.IsNullOrWhiteSpace(attribute.Description)
            ? throw new EnumDescriptionNotFoundException()
            : attribute.Description;
    }

    /// <summary>
    /// Retorna uma lista com os valores contidos no enumerador.
    /// </summary>
    /// <param name="value">
    /// Enum a ser transformado em uma lista.
    /// </param>
    /// <returns>
    /// Lista dos itens do enumerador.
    /// </returns>
    public static List<EnumModel> GetValuesAndDescriptions(
        this Enum value
    )
    {
        ArgumentNullException.ThrowIfNull(value);

        return Enum.GetValues(value.GetType())
            .Cast<Enum>()
            .Select(static (e) => new EnumModel(e, e.Description()))
            .ToList();
    }
}
