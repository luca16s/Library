// -----------------------------------------------------------------------
// <copyright file="Message.cs" company="Îakaré Software'Oka">
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
    using System.Text.Json.Serialization;

    /// <summary>
    /// Classe base da mensagem.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public abstract class Message<TId> : IRequest<bool> where TId : struct
    {
        /// <summary>
        /// Id da agregação.
        /// </summary>
        [JsonIgnore]
        public TId AggregateId { get; protected set; }

        /// <summary>
        /// Tipo da mensagem.
        /// </summary>
        [JsonIgnore]
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
