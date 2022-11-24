namespace Mediator.Commands
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Classe base de Comando de Query.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class QueryCommand<TId, TResponse> : Command<TId, TResponse>
        where TId : struct
        where TResponse : notnull
    {
        /// <summary>
        /// Quantidade de itens a serem trabalhados.
        /// </summary>
        [JsonIgnore]
        public int ItemAmount { get; set; } = 25;
    }
}
