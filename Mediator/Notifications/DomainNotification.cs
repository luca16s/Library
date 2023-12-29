// -----------------------------------------------------------------------
// <copyright file="DomainNotification.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
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
public class DomainNotification(
    string id,
    string value
) : INotification
{
    /// <summary>
    /// Identificador da notificação.
    /// </summary>
    public string Id { get; private set; } = id;

    /// <summary>
    /// Valor da notificação.
    /// </summary>
    public string Value { get; private set; } = value;
}
