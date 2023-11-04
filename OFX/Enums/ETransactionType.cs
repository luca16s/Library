// ------------------------------------------------------------------------------------
// <copyright file="ETransactionType.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Enums
{
    using System.ComponentModel;

    public enum ETransactionType
    {
        [Description("Basic Credit")]
        CREDIT,
        [Description("Basic Debit")]
        DEBIT,
        [Description("Interest")]
        INT,
        [Description("Dividend")]
        DIV,
        [Description("Fee")]
        FEE,
        [Description("Service Charge")]
        SRVCHG,
        [Description("Deposit")]
        DEP,
        [Description("ATM transfer")]
        ATM,
        [Description("Point of Sale transfer")]
        POS,
        [Description("Transfer")]
        XFER,
        [Description("Check")]
        CHECK,
        [Description("Payment")]
        PAYMENT,
        [Description("Cash Withdrawl")]
        CASH,
        [Description("Direct Deposit")]
        DIRECTDEP,
        [Description("Merchant Initiated Debit")]
        DIRECTDEBIT,
        [Description("Repeating Payment")]
        REPEATPMT,
        OTHER,
    }
}
