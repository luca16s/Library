// -----------------------------------------------------------------------
// <copyright file="BaseMessage.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Events;

using System.Text.Json.Serialization;

/// <summary>
/// Classe base de mensagem.
/// </summary>
public abstract class BaseMessage
{
    /// <summary>
    /// Inicializa uma nova instância da classe Message.
    /// </summary>
    protected BaseMessage()
    {
        MessageType = GetType().Name;
    }

    /// <summary>
    /// Identificador da mensagem.
    /// </summary>
    [JsonIgnore]
    public long Id { get; protected set; }

    /// <summary>
    /// Tipo da mensagem.
    /// </summary>
    [JsonIgnore]
    public string MessageType { get; protected set; }

    /// <summary>
    /// Timestamp da execução do comando.
    /// </summary>
    [JsonIgnore]
    protected DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}
