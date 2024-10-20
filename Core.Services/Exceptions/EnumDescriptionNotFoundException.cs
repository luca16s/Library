// ------------------------------------------------------------------------------------
// <copyright file="EnumDescriptionNotFoundException.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace Core.Services.Exceptions;

using Core.Services.Extensions;
using Core.Services.Properties;

/// <summary>
/// Exceção caso descrição do enum não tenha sido encontrada.
/// </summary>
public class EnumDescriptionNotFoundException : Exception
{
    /// <summary>
    /// Inicia uma nova instância da classe <see cref="EnumDescriptionNotFoundException" />.
    /// </summary>
    public EnumDescriptionNotFoundException()
        : base(Resources.EnumDescriptionNotFound) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="EnumDescriptionNotFoundException" />.
    /// </summary>
    /// <param name="message">
    /// Mensagem a ser mostrada.
    /// </param>
    public EnumDescriptionNotFoundException(
        string message
    ) : base(Resources.EnumBaseMessage.FormatMessage(Resources.EnumDescriptionNotFound, message)) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="EnumDescriptionNotFoundException" />.
    /// </summary>
    /// <param name="message">
    /// Mensagem a ser mostrada.
    /// </param>
    /// <param name="inner">
    /// Mensagem herdada.
    /// </param>
    public EnumDescriptionNotFoundException(
        string message,
        Exception inner
    ) : base(Resources.EnumBaseMessage.FormatMessage(Resources.EnumDescriptionNotFound, message), inner) { }
}
