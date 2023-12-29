// -----------------------------------------------------------------------
// <copyright file="IMediatorHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Interfaces;

using Mediator.Commands;
using Mediator.Notifications;

using System.Threading.Tasks;

/// <summary>
/// Interface do manipulador de mediação.
/// </summary>
public interface IMediatorHandler
{
    /// <summary>
    /// Lançar notificação.
    /// </summary>
    /// <typeparam name="TReturn">
    /// Tipo da notificação.
    /// </typeparam>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    Task Raise<TReturn>(
        TReturn evento,
        CancellationToken cancellation = default
    ) where TReturn : DomainNotification;

    /// <summary>
    /// Lançar notificação de erro.
    /// </summary>
    /// <typeparam name="TReturn">
    /// Tipo da notificação.
    /// </typeparam>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    Task RaiseError<TReturn>(
        TReturn evento,
        CancellationToken cancellation = default
    ) where TReturn : ErrorNotification;

    /// <summary>
    /// Enviar comando com retorno.
    /// </summary>
    /// <typeparam name="TCommand">
    /// Tipo do Evento.
    /// </typeparam>
    /// <typeparam name="TReturn">
    /// Tipo do retorno.
    /// </typeparam>
    /// <param name="comando">
    /// Comando a ser enviado.
    /// </param>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    Task<TReturn> Send<TCommand, TReturn>(
        TCommand comando,
        CancellationToken cancellation = default
    ) where TCommand : QueryCommand<TReturn>
      where TReturn : notnull;

    /// <summary>
    /// Enviar comando sem retorno.
    /// </summary>
    /// <typeparam name="TCommand">
    /// Tipo do Evento.
    /// </typeparam>
    /// <param name="comando">
    /// Comando a ser enviado.
    /// </param>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    Task Send<TCommand>(
        TCommand comando,
        CancellationToken cancellation = default
    ) where TCommand : Command;

    /// <summary>
    /// Publicar comando em uma fila.
    /// </summary>
    /// <typeparam name="TCommand">
    /// Tipo do comando.
    /// </typeparam>
    /// <param name="comando">
    /// Comando a ser publicado.
    /// </param>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    Task PublishQueue<TCommand>(
        TCommand comando,
        CancellationToken cancellation = default
    );
}
