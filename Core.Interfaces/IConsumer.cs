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
    Task<TResponse?> GetAsync(
        string url,
        string requestUri,
        string token,
        int requestTimeout,
        ApplicationInfo appInfo
    );

    Task<TResponse?> PostAsync<TContent>(
        string url,
        string requestUri,
        string token,
        TContent content,
        int? requestTimeout,
        ApplicationInfo appInfo
    );
}
