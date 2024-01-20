// -----------------------------------------------------------------------
// <copyright file="EnumExtension.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Extensions;
using Core.Exceptions;
using Core.Models;

using System.ComponentModel;

/// <summary>
/// Classe de extensão para operações com enumeradores.
/// </summary>
public static class EnumExtension
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
    public static string Description(this Enum value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Valor do enum não pode ser nulo.");
        }

        Type valueType = value.GetType();
        System.Reflection.FieldInfo field = valueType.GetField(value.ToString()) ?? throw new NullReferenceException("Field não é válido.");
        object[] attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false) ?? Array.Empty<Array>();

        return attributes.Length > 0 &&
            attributes.First() is DescriptionAttribute description
            ? description.Description
            : throw new EnumDescriptionNotFoundException();
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
    public static IEnumerable<EnumModel> GetAllValuesAndDescriptions(this Enum value)
    {
        return Enum.GetValues(value.GetType()).Cast<Enum>().Select((e)
            => new EnumModel()
            {
                Value = e,
                Description = e.Description()
            }).ToList();
    }
}
