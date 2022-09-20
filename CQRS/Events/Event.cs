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
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public abstract class Event<TId> : Message<TId>, INotification where TId : struct
    {
        /// <summary>
        /// Timestamp de execução do evento.
        /// </summary>
        protected DateTime Timestamp { get; private set; } = DateTime.UtcNow;
    }
}
