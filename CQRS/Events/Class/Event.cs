// -----------------------------------------------------------------------
// <copyright file="Event.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Events.Class
{
    using MediatR;

    using System;

    /// <summary>
    /// Classe base de Evento.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class Event<TId, TResponse> : Message<TId, TResponse>, INotification
        where TId : struct
        where TResponse : class
    {
        /// <summary>
        /// Timestamp de execução do evento.
        /// </summary>
        protected DateTime Timestamp { get; private set; } = DateTime.UtcNow;
    }
}
