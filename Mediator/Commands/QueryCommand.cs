namespace Mediator.Commands;

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
