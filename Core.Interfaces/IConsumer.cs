namespace Core.Interfaces;

using Core.CrossCutting;

using System.Threading.Tasks;

/// <summary>
/// Realiza o consumo de uma API web.
/// </summary>
/// <typeparam name="TResponse">
/// Tipo a ser retornado.
/// </typeparam>
public interface IConsumer<TResponse> where TResponse : notnull
{
    /// <summary>
    /// Realiza a consulta e transforma os dados de uma requisição.
    /// </summary>
    /// <param name="url">
    /// Url base para requisição.
    /// </param>
    /// <param name="requestUri">
    /// Rota para fazer a request.
    /// </param>
    /// <param name="appInfo">
    /// Informações da Aplicação.
    /// </param>
    /// <returns>
    /// Retorna o resultado da API.
    /// </returns>
    Task<TResponse> Consume(
        string url,
        string requestUri,
        ApplicationInfo appInfo
    );
}
