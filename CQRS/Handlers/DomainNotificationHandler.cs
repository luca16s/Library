// -----------------------------------------------------------------------
// <copyright file="CommandHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Handlers
{
    using CQRS.Interfaces;
    using CQRS.Notifications;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Classe manipuladora de notificação de domínio.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public class DomainNotificationHandler<TId, TResponse> : IDomainNotificationHandler<TId, TResponse>
        where TId : struct
        where TResponse : notnull
    {
        private List<DomainNotification<TId, TResponse>> _notifications;

        /// <summary>
        /// Constrói uma nova instância da classe manipuladora de notificação de domínio.
        /// </summary>
        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification<TId, TResponse>>();
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
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// Tipo do retorno.
        /// </typeparam>
        /// <returns>
        /// Lista das notificações adicionadas.
        /// </returns>
        public virtual List<DomainNotification<TId, TResponse>> GetNotifications()
        {
            return _notifications;
        }

        /// <summary>
        /// Manipulador de notificação de domínio.
        /// </summary>
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// Tipo do retorno.
        /// </typeparam>
        /// <param name="message">
        /// Mensagem a ser adicionada.
        /// </param>
        /// <param name="cancellationToken">
        /// Token de cancelamento.
        /// </param>
        public Task Handle(DomainNotification<TId, TResponse> message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Descarta manipulador de notificação de domínio.
        /// </summary>
        public void Dispose()
        {
            _notifications = new List<DomainNotification<TId, TResponse>>();
        }
    }
}
