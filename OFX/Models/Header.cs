// ------------------------------------------------------------------------------------
// <copyright file="Header.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace OFX.Models;

using OFX.Enums;

using System;

public class Header
{
    /// <summary>
    /// CODE: LANGUAGE
    /// <br/><br/>
    /// Linguá utilizada na exportação do arquivo.
    /// </summary>
    public ELanguage Language { get; private set; } = ELanguage.POR;

    /// <summary>
    /// CODE: DTSERVER
    /// <br/><br/>
    /// Data de geração do arquivo no formato yyyymmddhhmmss[-3:BRT].
    /// </summary>
    public DateTime ServerDate { get; private set; } = DateTime.Now;

    public Header() { }

    public void Add(
        string? language,
        DateTime serverDate
    )
    {
        if (!Enum.TryParse(language, out ELanguage languageCode))
            throw new Exception($"Código da linguagem não encontrado: {language}");

        Language = languageCode;
        ServerDate = serverDate;
    }
}
