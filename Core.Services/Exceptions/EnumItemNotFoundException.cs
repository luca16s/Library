// -----------------------------------------------------------------------
// <copyright file="EnumItemNotFoundException.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Services.Exceptions;

using Core.Services.Extensions;
using Core.Services.Properties;

/// <summary>
/// Exceção caso item do enum não tenha sido encontrado.
/// </summary>
public class EnumItemNotFoundException : Exception
{
    /// <summary>
    /// Inicia uma nova instância da classe <see cref="EnumItemNotFoundException" />.
    /// </summary>
    public EnumItemNotFoundException()
        : base(Resources.EnumItemNotFound) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="EnumItemNotFoundException" />.
    /// </summary>
    /// <param name="message">
    /// Mensagem a ser mostrada.
    /// </param>
    public EnumItemNotFoundException(
        string message
    ) : base(Resources.EnumBaseMessage.FormatText(Resources.EnumItemNotFound, message)) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="EnumItemNotFoundException" />.
    /// </summary>
    /// <param name="message">
    /// Mensagem a ser mostrada.
    /// </param>
    /// <param name="inner">
    /// Mensagem herdada.
    /// </param>
    public EnumItemNotFoundException(
        string message,
        Exception inner
    ) : base(Resources.EnumBaseMessage.FormatText(Resources.EnumItemNotFound, message), inner) { }
}
