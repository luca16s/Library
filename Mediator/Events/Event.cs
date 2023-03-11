// -----------------------------------------------------------------------
// <copyright file="Event.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Events
{
    using MediatR;

    /// <summary>
    /// Classe base de evento sem retorno.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public abstract class Event<TId> : Message<TId>, INotification where TId : struct { }

    /// <summary>
    /// Classe base de evento com retorno.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class Event<TId, TResponse> : Message<TId, TResponse>, INotification
        where TId : struct
        where TResponse : notnull
    { }
}
