// -----------------------------------------------------------------------
// <copyright file="IMediatorHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Interfaces
{
    using Mediator.Commands;
    using Mediator.Events;

    using System.Threading.Tasks;

    /// <summary>
    /// Interface do manipulador de mediação.
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// Lançar evento com retorno.
        /// </summary>
        /// <typeparam name="TCommand">
        /// Tipo do Evento.
        /// </typeparam>
        /// <typeparam name="TReturn">
        /// Tipo do retorno.
        /// </typeparam>
        /// <param name="evento">
        /// Evento a ser lançado.
        /// </param>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        Task Raise<TCommand, TReturn>(
            TCommand evento,
            CancellationToken cancellation = default
        ) where TReturn : notnull
          where TCommand : Event<TReturn>;

        /// <summary>
        /// Lançar evento sem retorno.
        /// </summary>
        /// <typeparam name="TReturn">
        /// Tipo do Evento.
        /// </typeparam>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        Task Raise<TReturn>(
            TReturn evento,
            CancellationToken cancellation = default
        ) where TReturn : Event;

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
}
