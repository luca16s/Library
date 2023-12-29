// ------------------------------------------------------------------------------------
// <copyright file="Balance.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Models;

using System.Globalization;

public class Balance
{
    /// <summary>
    /// CODE: BALAMT
    /// <br/><br/>
    /// Representa o saldo parcial da conta no momento da transação mais recente.
    /// <br/><br/>
    /// Exemplos:
    /// <br/><br/>
    /// 1050.32, -223.95
    /// </summary>
    public decimal TotalHeld { get; private set; } = 0;

    /// <summary>
    /// CODE: DTASOF
    /// <br/><br/>
    /// Data de lançamento da transação mais recente no formato yyyymmdd120000[-3:BRT].
    /// </summary>
    public DateTime PayoffDate { get; private set; } = DateTime.Now;

    public Balance() { }

    public void Add(
        string totalHeld,
        DateTime payoff
    )
    {
        _ = decimal.TryParse(totalHeld, CultureInfo.InvariantCulture, out var saldo);

        TotalHeld = saldo;
        PayoffDate = payoff;
    }
}
