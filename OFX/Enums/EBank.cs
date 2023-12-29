// ------------------------------------------------------------------------------------
// <copyright file="EBank.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Enums;

using System.ComponentModel;

/// <summary>
/// Correspondência de Códigos e Nomes dos bancos.
/// </summary>
public enum EBank
{
    /// <summary>
    /// Itaú Unibanco.
    /// </summary>
    [Description("ITAÚ UNIBANCO")]
    ITAU = 341,

    /// <summary>
    /// Caixa Econômica Federal.
    /// </summary>
    [Description("CAIXA ECONOMICA FEDERAL")]
    CAIXA = 104,
}
