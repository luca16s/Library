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
    public abstract class Event : Message, INotification { }

    /// <summary>
    /// Classe base de evento com retorno.
    /// </summary>
    /// <typeparam name="TReturn">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class Event<TReturn> : Message<TReturn>, INotification
        where TReturn : notnull
    { }
}
