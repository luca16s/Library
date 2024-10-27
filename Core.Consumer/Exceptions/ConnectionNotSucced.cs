// -----------------------------------------------------------------------
// <copyright file="ConnectionNotSucced.cs" company="Îakaré Softwareoka Inc.">
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
/// Exceção caso conexão com URL não tenha sucesso.
/// </summary>
public class ConnectionNotSucced : Exception
{
    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ConnectionNotSucced" />.
    /// </summary>
    public ConnectionNotSucced()
        : base(Resources.ConnectionNotSucced) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ConnectionNotSucced" />.
    /// </summary>
    /// <param name="url">
    /// Url utilizada.
    /// </param>
    public ConnectionNotSucced(
        string url
    ) : base(Resources.ConnectionNotSucced.FormatText(url)) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ConnectionNotSucced" />.
    /// </summary>
    /// <param name="url">
    /// Url utilizada.
    /// </param>
    /// <param name="inner">
    /// Mensagem herdada.
    /// </param>
    public ConnectionNotSucced(
        string url,
        Exception inner
    ) : base(Resources.ConnectionNotSucced.FormatText(url), inner) { }
}
