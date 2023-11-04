// ------------------------------------------------------------------------------------
// <copyright file="EBankType.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// Tipos de contas.
    /// </summary>
    public enum EBankType
    {
        [Description("Cartão de Crédito")]
        CC = 0,
        [Description("Conta Bancária")]
        BANK = 1,
        [Description("Conta de Pagamento")]
        AP = 2,
        NA = 3,
    }
}
