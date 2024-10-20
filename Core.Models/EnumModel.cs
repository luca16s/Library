// -----------------------------------------------------------------------
// <copyright file="EnumModel.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Models;

/// <summary>
/// Classe modelo para conversão de enumerador em lista.
/// </summary>
public class EnumModel(
    Enum enumValue,
    string description
)
{
    /// <summary>
    /// Valor do enum.
    /// </summary>
    public Enum Value { get; } = enumValue;

    /// <summary>
    /// Descrição do enum.
    /// </summary>
    public string Description { get; } = description;
}
