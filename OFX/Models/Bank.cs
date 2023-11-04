// ------------------------------------------------------------------------------------
// <copyright file="Bank.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Models
{
    using Core.Extensions;

    using OFX.Enums;

    public class Bank
    {
        /// <summary>
        /// CODE: BANKID
        /// <br/><br/>
        /// </summary>
        public EBank Code { get; set; } = EBank.CAIXA;

        /// <summary>
        /// Nome do Banco indicado no campo <see cref="Code"/>
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public Bank() { }

        public void Add(
            string? bankCode
        )
        {
            if (!Enum.TryParse(bankCode, out EBank code))
                throw new Exception($"Código bancário não encontrado: {bankCode}");

            Code = code;
            Name = ToString();
        }

        public override string ToString()
        {
            try
            {
                return Code.Description();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
