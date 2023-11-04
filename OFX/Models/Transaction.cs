// ------------------------------------------------------------------------------------
// <copyright file="Transaction.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Models
{
    using OFX.Enums;

    using System;
    using System.Globalization;

    public class Transaction
    {
        /// <summary>
        /// CODE: FITID
        /// <br/><br/>
        /// Código único da transação com 34 caracteres, dispostos da seguinte forma:
        /// <br/><br/>
        /// - 12 caracteres para a data;
        /// - 7 caracteres para o código da transação;
        /// - 15 caracteres para o protocolo
        /// <br/><br/>
        /// Exemplo:
        /// <br/><br/>
        /// - Data: 28/03/2018 08:05
        /// - Código da transação: 2002001 (vide tabela completa)
        /// - Protocol: 63773485
        /// <br/><br/>
        /// FITID: 201803280805 2002001 000000063773485 (sem espaços)
        /// </summary>
        public string Id { get; private set; } = string.Empty;

        /// <summary>
        /// CODE: TRNAMT
        /// <br/><br/>
        /// Valor da transação com duas casas decimais para os centavos (separados por ponto).
        /// Se a transação for um débito, haverá um sinal negativo no valor.
        /// <br/><br/>
        /// Exemplos:
        /// <br/><br/>
        /// - Crédito: 123.45 equivale a R$ 123,45
        /// - Débito: -84.30 equivale a -R$ 84,30
        /// </summary>
        public decimal Value { get; private set; } = 0;

        /// <summary>
        /// CODE: DTPOSTED
        /// Data de lançamento da transação. Ou seja, a data de disponibilização do valor na conta corrente.
        /// <br/><br/>
        /// Formato: yyyymmdd120000[-3:BRT].
        /// </summary>
        public DateTime Date { get; private set; } = DateTime.Now;

        /// <summary>
        /// CODE: CHECKNUM
        /// <br/><br/>
        /// Protocol da transação com 15 caracteres.
        /// <br/><br/>
        /// Exemplo:
        /// <br/><br/>
        /// - Protocol: 63773485
        /// <br/><br/>
        /// - CHECKNUM: 000000063773485
        /// </summary>
        public long Protocol { get; private set; } = 0;

        /// <summary>
        /// CODE: TRNTYPE
        /// <br/><br/>
        /// Aceita os valores CREDIT ou DEBIT.
        /// </summary>
        public ETransactionType Type { get; private set; } = ETransactionType.CASH;

        /// <summary>
        /// CODE: MEMO
        /// <br/><br/>
        /// Descrição da cobrança.
        /// </summary>
        public string Description { get; private set; } = string.Empty;

        public Transaction() { }

        public void Add(
            string id,
            string? type,
            string? value,
            DateTime date,
            string? checknum,
            string? description
        )
        {
            _ = long.TryParse(checknum, out var checkNum);
            _ = decimal.TryParse(value, CultureInfo.InvariantCulture, out var transactionValue);

            if (!Enum.TryParse(type, out ETransactionType typeCode))
                throw new Exception($"Código bancário não encontrado: {type}");

            Id = id;
            Date = date;
            Type = typeCode;
            Protocol = checkNum;
            Value = transactionValue;
            Description = description ?? string.Empty;
        }
    }
}
