// -----------------------------------------------------------------------
// <copyright file="DomainNotification.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Notifications;

using MediatR;

/// <summary>
/// Notificação de domínio sem retorno.
/// </summary>
/// <remarks>
/// Construtor da classe de Notificação de domínio.
/// </remarks>
/// <param name="key">
/// Chave da notificação.
/// </param>
/// <param name="value">
/// Valor da notificação.
/// </param>
public class DomainNotification : INotification
{
    /// <summary>
    /// Identificador da notificação.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Valor da notificação.
    /// </summary>
    public required string Value { get; set; }
}
