// -----------------------------------------------------------------------
// <copyright file="DomainNotification.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Notifications;

using Mediator.Events;

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
    string key,
    string value
    ) : Event
{
    /// <summary>
    /// Identificador da notigicação.
    /// </summary>
    public long NotificationId { get; private set; }

    /// <summary>
    /// Chave da notificação.
    /// </summary>
    public string Key { get; private set; } = key;

    /// <summary>
    /// Valor da notificação.
    /// </summary>
    public string Value { get; private set; } = value;

    /// <summary>
    /// Versão da notificação.
    /// </summary>
    public int Version { get; private set; } = 1;
}

/// <summary>
/// Notificação de domínio com retorno.
/// </summary>
/// <typeparam name="TReturn">
/// Tipo do retorno.
/// </typeparam>
/// <remarks>
/// Construtor da classe de Notificação de domínio.
/// </remarks>
/// <param name="key">
/// Chave da notificação.
/// </param>
/// <param name="value">
/// Valor da notificação.
/// </param>
public class DomainNotification<TReturn>(
    string key,
    string value
    ) : Event<TReturn>
    where TReturn : notnull
{
    /// <summary>
    /// Identificador da notigicação.
    /// </summary>
    public long NotificationId { get; private set; }

    /// <summary>
    /// Chave da notificação.
    /// </summary>
    public string Key { get; private set; } = key;

    /// <summary>
    /// Valor da notificação.
    /// </summary>
    public string Value { get; private set; } = value;

    /// <summary>
    /// Versão da notificação.
    /// </summary>
    public int Version { get; private set; } = 1;
}
