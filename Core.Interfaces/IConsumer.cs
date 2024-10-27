namespace Core.Interfaces;

using Core.CrossCutting;

/// <summary>
/// Realiza o consumo de uma API web.
/// </summary>
/// <typeparam name="TResponse">
/// Tipo a ser retornado.
/// </typeparam>
public interface IConsumer<TResponse> where TResponse : notnull
{
    /// <summary>
    /// Realiza uma solicitação GET assíncrona para a API web.
    /// </summary>
    /// <param name="url">A URL base da API.</param>
    /// <param name="requestUri">O URI da solicitação.</param>
    /// <param name="requestTimeout">O tempo limite da solicitação em milissegundos.</param>
    /// <param name="appInfo">Informações da aplicação que está fazendo a solicitação.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta da API.</returns>
    Task<TResponse?> GetAsync(
        string url,
        string requestUri,
        int requestTimeout,
        ApplicationInfo appInfo
    );

    /// <summary>
    /// Realiza uma solicitação POST assíncrona para a API web.
    /// </summary>
    /// <typeparam name="TContent">Tipo do conteúdo a ser enviado na solicitação.</typeparam>
    /// <param name="url">A URL base da API.</param>
    /// <param name="requestUri">O URI da solicitação.</param>
    /// <param name="content">O conteúdo a ser enviado na solicitação.</param>
    /// <param name="requestTimeout">O tempo limite da solicitação em milissegundos.</param>
    /// <param name="appInfo">Informações da aplicação que está fazendo a solicitação.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta da API.</returns>
    Task<TResponse?> PostAsync<TContent>(
        string url,
        string requestUri,
        TContent content,
        int? requestTimeout,
        ApplicationInfo appInfo
    );

    /// <summary>
    /// Realiza uma solicitação GET assíncrona para a API web.
    /// </summary>
    /// <param name="url">A URL base da API.</param>
    /// <param name="token">O token de autenticação.</param>
    /// <param name="requestUri">O URI da solicitação.</param>
    /// <param name="requestTimeout">O tempo limite da solicitação em milissegundos.</param>
    /// <param name="appInfo">Informações da aplicação que está fazendo a solicitação.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta da API.</returns>
    Task<TResponse?> GetAsync(
        string url,
        string requestUri,
        string token,
        int requestTimeout,
        ApplicationInfo appInfo
    );

    /// <summary>
    /// Realiza uma solicitação POST assíncrona para a API web.
    /// </summary>
    /// <typeparam name="TContent">Tipo do conteúdo a ser enviado na solicitação.</typeparam>
    /// <param name="url">A URL base da API.</param>
    /// <param name="requestUri">O URI da solicitação.</param>
    /// <param name="token">O token de autenticação.</param>
    /// <param name="content">O conteúdo a ser enviado na solicitação.</param>
    /// <param name="requestTimeout">O tempo limite da solicitação em milissegundos.</param>
    /// <param name="appInfo">Informações da aplicação que está fazendo a solicitação.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. O resultado contém a resposta da API.</returns>
    Task<TResponse?> PostAsync<TContent>(
        string url,
        string requestUri,
        string token,
        TContent content,
        int? requestTimeout,
        ApplicationInfo appInfo
    );
}
