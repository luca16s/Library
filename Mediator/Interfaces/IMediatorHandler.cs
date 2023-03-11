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
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// Tipo do retorno.
        /// </typeparam>
        /// <param name="evento">
        /// Evento a ser lançado.
        /// </param>
        /// <param name="enqueue">
        /// Deve enfileirar?
        /// </param>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        Task RaiseEvent<TCommand, TId, TResponse>(
            TCommand evento,
            CancellationToken cancellation = default
        ) where TId : struct
          where TResponse : notnull
          where TCommand : Event<TId, TResponse>;

        /// <summary>
        /// Lançar evento sem retorno.
        /// </summary>
        /// <typeparam name="TCommand">
        /// Tipo do Evento.
        /// </typeparam>
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// Tipo do retorno.
        /// </typeparam>
        /// <param name="enqueue">
        /// Deve enfileirar?
        /// </param>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        Task RaiseEvent<TCommand, TId>(
            TCommand evento,
            CancellationToken cancellation = default
        ) where TId : struct
          where TCommand : Event<TId>;

        /// <summary>
        /// Enviar comando com retorno.
        /// </summary>
        /// <typeparam name="TCommand">
        /// Tipo do Evento.
        /// </typeparam>
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// Tipo do retorno.
        /// </typeparam>
        /// <param name="comando">
        /// Comando a ser enviado.
        /// </param>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        Task<TResponse> SendCommand<TCommand, TId, TResponse>(
            TCommand comando,
            CancellationToken cancellation = default
        ) where TCommand : QueryCommand<TId, TResponse>
            where TResponse : notnull
            where TId : struct;

        /// <summary>
        /// Enviar comando sem retorno.
        /// </summary>
        /// <typeparam name="TCommand">
        /// Tipo do Evento.
        /// </typeparam>
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// Tipo do retorno.
        /// </typeparam>
        /// <param name="comando">
        /// Comando a ser enviado.
        /// </param>
        /// <param name="enqueue">
        /// Deve enfileirar?
        /// </param>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        Task SendCommand<TCommand, TId>(
            TCommand comando,
            CancellationToken cancellation = default
        ) where TId : struct
          where TCommand : Command<TId>;

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
