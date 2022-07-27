// -----------------------------------------------------------------------
// <copyright file="DomainNotification.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Notifications
{
    using CQRS.Events;

    using System;

    /// <summary>
    /// Notificação de domínio.
    /// </summary>
    public class DomainNotification : Event
    {
        /// <summary>
        /// Identificador da notigicação.
        /// </summary>
        public Guid NotificationId { get; private set; } = Guid.NewGuid();

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
