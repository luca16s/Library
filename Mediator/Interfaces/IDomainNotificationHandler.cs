// -----------------------------------------------------------------------
// <copyright file="IDomainNotificationHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Interfaces;

using Mediator.Notifications;

using MediatR;

/// <summary>
/// Interface para gerenciamento da notificação de domínio.
/// </summary>
public interface IDomainNotificationHandler : INotificationHandler<INotification>
{
    /// <summary>
    /// Verifica se existem notificações.
    /// </summary>
    /// <returns>
    /// Retorna True caso existam notificações.
    /// </returns>
    bool HasNotifications();

    /// <summary>
    /// Limpar notificações.
    /// </summary>
    void ClearNotifications();

    /// <summary>
    /// Pega as notificações.
    /// </summary>
    /// <returns>
    /// Retorna a lista de notificações.
    /// </returns>
    List<INotification> GetNotifications();
}
