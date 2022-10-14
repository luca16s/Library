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

    using System.Text.Json.Serialization;

    /// <summary>
    /// Classe base da mensagem.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class Message<TId, TResponse> : IRequest<TResponse>
        where TId : struct
        where TResponse : notnull
    {
        /// <summary>
        /// Identificador.
        /// </summary>
        [JsonIgnore]
        public TId Id { get; protected set; }

        /// <summary>
        /// Tipo da mensagem.
        /// </summary>
        [JsonIgnore]
        public string MessageType { get; protected set; }

        /// <summary>
        /// Inicializa uma nova instância da classe Message.
        /// </summary>
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
