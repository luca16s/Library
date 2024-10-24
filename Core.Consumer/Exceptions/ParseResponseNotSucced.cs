namespace Core.Consumer.Exceptions;

using Core.Consumer.Properties;
using Core.Services.Extensions;

using System;

/// <summary>
/// Exceção parse do resultado da API não tenha compatibilidade com modelo passado.
/// </summary>
public class ParseResponseNotSucced : Exception
{
    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ParseResponseNotSucced" />.
    /// </summary>
    public ParseResponseNotSucced()
        : base(Resources.ParseErrorMessage) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ParseResponseNotSucced" />.
    /// </summary>
    /// <param name="url">
    /// Url utilizada.
    /// </param>
    /// <param name="tipo">
    /// Tipo utilizado na conversão.
    /// </param>
    public ParseResponseNotSucced(
        string url,
        string tipo
    ) : base(Resources.ParseErrorMessage.FormatMessage(tipo, url)) { }

    /// <summary>
    /// Inicia uma nova instância da classe <see cref="ParseResponseNotSucced" />.
    /// </summary>
    /// <param name="url">
    /// Url utilizada.
    /// </param>
    /// <param name="tipo">
    /// Tipo utilizado na conversão.
    /// </param>
    /// <param name="inner">
    /// Mensagem herdada.
    /// </param>
    public ParseResponseNotSucced(
        string url,
        string tipo,
        Exception inner
    ) : base(Resources.ParseErrorMessage.FormatMessage(tipo, url), inner) { }
}
