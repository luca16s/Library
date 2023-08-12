// -----------------------------------------------------------------------
// <copyright file="CommandHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Handlers
{
    using Mediator.Interfaces;
    using Mediator.Notifications;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Classe manipuladora de notificação de domínio sem resposta.
    /// </summary>
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private List<DomainNotification> _notifications;

        /// <summary>
        /// Constrói uma nova instância da classe manipuladora de notificação de domínio.
        /// </summary>
        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        /// <summary>
        /// Verifica se tem notificações.
        /// </summary>
        /// <returns>
        /// True: Contém notificações.
        /// False: Não contém notificações.
        /// </returns>
        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        /// <summary>
        /// Limpa notificações.
        /// </summary>
        public virtual void ClearNotifications()
        {
            _notifications.Clear();
        }

        /// <summary>
        /// Busca todas as notificações.
        /// </summary>
        /// <returns>
        /// Lista das notificações adicionadas.
        /// </returns>
        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
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
        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Descarta manipulador de notificação de domínio.
        /// </summary>
        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }

    /// <summary>
    /// Classe manipuladora de notificação de domínio com resposta.
    /// </summary>
    /// <typeparam name="TReturn">
    /// Tipo do retorno.
    /// </typeparam>
    public class DomainNotificationHandler<TReturn> : IDomainNotificationHandler<TReturn>
        where TReturn : notnull
    {
        private List<DomainNotification<TReturn>> _notifications;

        /// <summary>
        /// Constrói uma nova instância da classe manipuladora de notificação de domínio.
        /// </summary>
        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification<TReturn>>();
        }

        /// <summary>
        /// Verifica se tem notificações.
        /// </summary>
        /// <returns>
        /// True: Contém notificações.
        /// False: Não contém notificações.
        /// </returns>
        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        /// <summary>
        /// Limpa notificações.
        /// </summary>
        public virtual void ClearNotifications()
        {
            _notifications.Clear();
        }

        /// <summary>
        /// Busca todas as notificações.
        /// </summary>
        /// <returns>
        /// Lista das notificações adicionadas.
        /// </returns>
        public virtual List<DomainNotification<TReturn>> GetNotifications()
        {
            return _notifications;
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
        public Task Handle(DomainNotification<TReturn> message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Descarta manipulador de notificação de domínio.
        /// </summary>
        public void Dispose()
        {
            _notifications = new List<DomainNotification<TReturn>>();
        }
    }
}
