// ------------------------------------------------------------------------------------
// <copyright file="EAccountType.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Enums;

using System.ComponentModel;

public enum EAccountType
{
    /// <summary>
    /// Conta Corrente.
    /// </summary>
    [Description("Conta Corrente")]
    CHECKING = 1,

    /// <summary>
    /// Conta de Crédito.
    /// </summary>
    [Description("Conta de Crédito")]
    CREDIT = 2,

    /// <summary>
    /// Conta poupança.
    /// </summary>
    [Description("Conta Poupança")]
    SAVINGS = 3
}
