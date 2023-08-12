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
    public abstract class Message : BaseMessage, IRequest
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
    /// <typeparam name="TReturn">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class Message<TReturn> : BaseMessage, IRequest<TReturn>
        where TReturn : notnull
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
