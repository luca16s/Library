// -----------------------------------------------------------------------
// <copyright file="CommandHandler.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Handlers;

using Mediator.Interfaces;
using Mediator.Notifications;

using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Classe manipuladora de notificação de domínio.
/// </summary>
public class DomainNotificationHandler : IDomainNotificationHandler
{
    private List<DomainNotification> notifications;

    /// <summary>
    /// Constrói uma nova instância da classe manipuladora de notificação de domínio.
    /// </summary>
    public DomainNotificationHandler(
    )
    {
        notifications = [];
    }

    /// <summary>
    /// Verifica se tem notificações.
    /// </summary>
    /// <returns>
    /// True: Contém notificações.
    /// False: Não contém notificações.
    /// </returns>
    public virtual bool HasNotifications(
    )
    {
        return notifications.Count != 0;
    }

    /// <summary>
    /// Limpa notificações.
    /// </summary>
    public virtual void ClearNotifications(
    )
    {
        notifications.Clear();
    }

    /// <summary>
    /// Busca todas as notificações.
    /// </summary>
    /// <returns>
    /// Lista das notificações adicionadas.
    /// </returns>
    public virtual List<DomainNotification> GetNotifications(
    )
    {
        return notifications;
    }

    /// <summary>
    /// Manipulador de notificação de domínio.
    /// </summary>
    /// <param name="message">
    /// Mensagem a ser adicionada.
    /// </param>
    /// <param name="cancellationToken">
    /// Token de cancelamento.
    /// </param>
    public Task Handle(
        DomainNotification message,
        CancellationToken cancellationToken
    )
    {
        notifications.Add(message);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Descarta manipulador de notificação de domínio.
    /// </summary>
    public void Dispose(
    )
    {
        notifications = [];
    }
}
