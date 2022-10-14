// -----------------------------------------------------------------------
// <copyright file="DomainNotification.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Notifications.Struct
{
    using CQRS.Events.Struct;

    /// <summary>
    /// Notificação de domínio.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public class DomainNotification<TId, TResponse> : Event<TId, TResponse>
        where TId : struct
        where TResponse : struct
    {
        /// <summary>
        /// Identificador da notigicação.
        /// </summary>
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        public TId NotificationId { get; private set; }

        /// <summary>
        /// Chave da notificação.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Valor da notificação.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Versão da notificação.
        /// </summary>
        public int Version { get; private set; } = 1;

        /// <summary>
        /// Construtor da classe de Notificação de domínio.
        /// </summary>
        /// <param name="key">
        /// Chave da notificação.
        /// </param>
        /// <param name="value">
        /// Valor da notificação.
        /// </param>
        public DomainNotification(string key,
                                  string value)
        {
            Key = key;
            Value = value;
        }
    }
}
