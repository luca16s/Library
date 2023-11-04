// ------------------------------------------------------------------------------------
// <copyright file="Account.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Models
{
    using OFX.Enums;

    public class Account
    {
        /// <summary>
        /// Classe com informações de conta bancária.
        /// <br/><br/>
        /// </summary>
        public Bank Bank { get; private set; } = new();

        /// <summary>
        /// CODE: ACCTID
        /// <br/><br/>
        /// Número da conta Efí.
        /// <br/><br/>
        /// Código da agência.
        /// </summary>
        public string AgencyCode { get; set; } = string.Empty;

        /// <summary>
        /// Tipo da Conta Bancária.
        /// <br/><br/>
        /// <see cref="EBankType.CC"/>
        /// <br/><br/>
        /// <see cref="EBankType.AP"/>
        /// <br/><br/>
        /// <see cref="EBankType.BANK"/>
        /// </summary>
        public EBankType BankType { get; set; } = EBankType.BANK;

        /// <summary>
        /// CODE: ACCTID
        /// <br/><br/>
        /// Número da conta Efí.
        /// <br/><br/>
        /// Código da conta bancária.
        /// </summary>
        public string AccountCode { get; set; } = string.Empty;

        /// <summary>
        /// CODE: ACCTTYPE
        /// <br/><br/>
        /// Tipo da conta bancária.
        /// </summary>
        public EAccountType AccountType { get; set; } = EAccountType.CHECKING;

        public Account() { }

        public void Add(
            string? eBankType,
            string? accountCode,
            string? eAccountType
        )
        {
            if (!Enum.TryParse(eBankType, out EBankType bankType))
                throw new ArgumentNullException(nameof(eBankType));

            if (string.IsNullOrWhiteSpace(accountCode))
                throw new ArgumentNullException(nameof(accountCode));

            if (!Enum.TryParse(eAccountType, out EAccountType accountType))
                throw new ArgumentNullException(nameof(eAccountType));

            BankType = bankType;
            AccountType = accountType;
            AgencyCode = accountCode[..4];
            AccountCode = $"{accountCode[4..9]}-{accountCode[9..]}";
        }

        public void AddBank(
            string? idBanco
        )
        {
            if (string.IsNullOrWhiteSpace(idBanco))
                throw new NullReferenceException($"As informações da conta bancária não podem ser nulas: {nameof(idBanco)}");

            Bank.Add(idBanco);
        }

        public override string ToString() => $"{Bank} | Agência: {AgencyCode}, Conta: {AccountCode}";
    }
}
