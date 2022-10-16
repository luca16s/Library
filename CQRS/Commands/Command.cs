// -----------------------------------------------------------------------
// <copyright file="Command.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Commands
{
    using CQRS.Events;

    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Classe base de Comando.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class Command<TId, TResponse> : Message<TId, TResponse>
        where TId : struct
        where TResponse : notnull
    {
        /// <summary>
        /// Timestamp de execução do comando.
        /// </summary>
        [JsonIgnore]
        protected DateTime Timestamp { get; private set; } = DateTime.UtcNow;
    }
}
