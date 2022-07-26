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
    public abstract class Command : Message
    {
        /// <summary>
        /// Resultado do comando.
        /// </summary>
        [JsonIgnore]
        public object? Result { get; set; }

        /// <summary>
        /// Timestamp de execução do comando.
        /// </summary>
        [JsonIgnore]
        protected DateTime Timestamp { get; private set; } = DateTime.UtcNow;
    }
}
