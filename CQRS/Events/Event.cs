// -----------------------------------------------------------------------
// <copyright file="Event.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Events
{
    using MediatR;

    using System;

    /// <summary>
    /// Classe base de Evento.
    /// </summary>
    public abstract class Event : Message, INotification
    {
        /// <summary>
        /// Timestamp de execução do evento.
        /// </summary>
        protected DateTime Timestamp { get; private set; } = DateTime.UtcNow;
    }
}
