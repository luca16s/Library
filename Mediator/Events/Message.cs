// -----------------------------------------------------------------------
// <copyright file="Message.cs" company="Îakaré Software'Oka">
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
    /// Classe base da mensagem sem retorno.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public abstract class Message<TId> : BaseMessage<TId>, IRequest
        where TId : struct
    {
        /// <summary>
        /// Inicializa uma nova instância da classe Message.
        /// </summary>
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }

    /// <summary>
    /// Classe base da mensagem com retorno.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class Message<TId, TResponse> : BaseMessage<TId>, IRequest<TResponse>
        where TId : struct
        where TResponse : notnull
    {
        /// <summary>
        /// Inicializa uma nova instância da classe Message.
        /// </summary>
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
