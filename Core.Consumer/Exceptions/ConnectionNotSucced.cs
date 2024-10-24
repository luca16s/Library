namespace Core.Consumer.Exceptions;

using Core.Consumer.Properties;
using Core.Services.Extensions;

using System;

/// <summary>
/// Exceção caso conexão com URL não tenha sucesso.
/// </summary>
public class ConnectionNotSucced : Exception
{
    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ConnectionNotSucced" />.
    /// </summary>
    public ConnectionNotSucced()
        : base(Resources.ConnectionNotSucced) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ConnectionNotSucced" />.
    /// </summary>
    /// <param name="url">
    /// Url utilizada.
    /// </param>
    public ConnectionNotSucced(
        string url
    ) : base(Resources.ConnectionNotSucced.FormatMessage(url)) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ConnectionNotSucced" />.
    /// </summary>
    /// <param name="url">
    /// Url utilizada.
    /// </param>
    /// <param name="inner">
    /// Mensagem herdada.
    /// </param>
    public ConnectionNotSucced(
        string url,
        Exception inner
    ) : base(Resources.ConnectionNotSucced.FormatMessage(url), inner) { }
}
