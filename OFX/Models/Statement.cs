// ------------------------------------------------------------------------------------
// <copyright file="Statement.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Models;

using OFX.Enums;

using System;
using System.Collections.Generic;

public class Statement
{
    /// <summary>
    /// Cabeçalho do arquivo OFX.
    /// <br/><br/>
    /// </summary>
    public Header Header { get; private set; } = new();

    /// <summary>
    /// Balanço geral da conta exportada.
    /// <br/><br/>
    /// </summary>
    public Balance Balance { get; private set; } = new();

    /// <summary>
    /// CODE: DTEND
    /// <br/><br/>
    /// Data do último registro de movimentação no formato yyyymmdd120000[-3:BRT].
    /// </summary>
    public DateTime FinalDate { get; private set; } = DateTime.Now;

    /// <summary>
    /// CODE: CURDEF
    /// <br/><br/>
    /// Moeda utilizada na exportação do arquivo.
    /// </summary>
    public ECurrency Currency { get; private set; } = ECurrency.BRL;

    /// <summary>
    /// Conta bancária exportada no arquivo OFX.
    /// <br/><br/>
    /// </summary>
    public Account Account { get; private set; } = new();

    /// <summary>
    /// CODE: DTSTART
    /// <br/><br/>
    /// Data do primeiro registro de movimentação no formato yyyymmdd120000[-3:BRT].
    /// </summary>
    public DateTime InitialDate { get; private set; } = DateTime.Now;

    /// <summary>
    /// Lista de erros ocorridos na importação.
    /// <br/><br/>
    /// </summary>
    public IList<string> ImportingErrors { get; private set; } = new List<string>();

    /// <summary>
    ///  List de transações realizadas na referida conta.
    /// <br/><br/>
    /// </summary>
    public List<Transaction> Transactions { get; private set; } = [];

    public Statement() { }

    internal void Add(
        string? currency,
        DateTime finalDate,
        DateTime initialDate
    )
    {
        if (!Enum.TryParse(currency, out ECurrency currencyCode))
            throw new Exception($"Código bancário não encontrado: {currency}");

        FinalDate = finalDate;
        Currency = currencyCode;
        InitialDate = initialDate;
    }

    internal void AddHeader(
        string language,
        DateTime date
    ) => Header.Add(language, date);

    internal void AddBalance(
        string balanco,
        DateTime date
    ) => Balance.Add(balanco, date);

    internal void AddBank(
        string idBanco
    ) => Account.AddBank(idBanco);

    internal void AddAccount(
        string? eBankType,
        string? accountCode,
        string? eAccountType
    ) => Account.Add(
        eBankType,
        accountCode,
        eAccountType
    );

    internal void AddTransaction(
        string id,
        string? type,
        string? value,
        DateTime date,
        string? checknum,
        string? description
    )
    {
        Transaction transacao = new();

        transacao.Add(
            id,
            type,
            value,
            date,
            checknum,
            description
        );

        Transactions.Add(
            transacao
        );
    }
}
