// -----------------------------------------------------------------------
// <copyright file="ParseResponseNotSucced.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Consumer.Exceptions;

using Core.Consumer.Properties;
using Core.Services.Extensions;

using System;

/// <summary>
/// Exceção parse do resultado da API não tenha compatibilidade com modelo passado.
/// </summary>
public class ParseResponseNotSucced : Exception
{
    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ParseResponseNotSucced" />.
    /// </summary>
    public ParseResponseNotSucced()
        : base(Resources.ParseErrorMessage) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ParseResponseNotSucced" />.
    /// </summary>
    /// <param name="url">
    /// Url utilizada.
    /// </param>
    /// <param name="tipo">
    /// Tipo utilizado na conversão.
    /// </param>
    public ParseResponseNotSucced(
        string url,
        string tipo
    ) : base(Resources.ParseErrorMessage.FormatText(tipo, url)) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ParseResponseNotSucced" />.
    /// </summary>
    /// <param name="url">
    /// Url utilizada.
    /// </param>
    /// <param name="tipo">
    /// Tipo utilizado na conversão.
    /// </param>
    /// <param name="inner">
    /// Mensagem herdada.
    /// </param>
    public ParseResponseNotSucced(
        string url,
        string tipo,
        Exception inner
    ) : base(Resources.ParseErrorMessage.FormatText(tipo, url), inner) { }
}
