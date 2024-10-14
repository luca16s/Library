// -----------------------------------------------------------------------
// <copyright file="BaseMessage.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Cqrs.Commands;

using System.Text.Json.Serialization;

/// <summary>
/// Classe base de mensagem.
/// </summary>
public abstract class BaseCommand
{
    /// <summary>
    /// Inicializa uma nova instância da classe Message.
    /// </summary>
    protected BaseCommand()
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
