// -----------------------------------------------------------------------
// <copyright file="QueryCommand.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Cqrs.Commands;

using MediatR;

using System.Text.Json.Serialization;

/// <summary>
/// Classe base de comando de query com retorno.
/// </summary>
/// <typeparam name="TReturn">
/// Tipo do retorno.
/// </typeparam>
public abstract class QueryCommand<TReturn> : BaseCommand, IRequest<TReturn>
    where TReturn : notnull
{
    /// <summary>
    /// Quantidade de itens a serem ignorados.
    /// </summary>
    [JsonIgnore]
    public int AmountToSkip { get; set; } = 0;

    /// <summary>
    /// Quantidade de itens a serem retornados.
    /// </summary>
    [JsonIgnore]
    public int AmountToTake { get; set; } = 25;

    /// <summary>
    /// Indice atual da busca.
    /// </summary>
    [JsonIgnore]
    public int ActualIndex => AmountToTake + ActualIndex;
}
