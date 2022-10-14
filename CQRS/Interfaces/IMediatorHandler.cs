// -----------------------------------------------------------------------
// <copyright file="IMediatorHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Interfaces
{
    using CQRS.Events;

    using System.Threading.Tasks;
    using CQRS.Commands;

    /// <summary>
    /// Interface do manipulador de mediação.
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// Lança o evento.
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
            bool enqueue = false,
            CancellationToken cancellation = default
        )
            where TCommand : Event<TId, TResponse>
            where TResponse : notnull
            where TId : struct;

        /// <summary>
        /// Envia o comando.
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
        Task SendCommand<TCommand, TId, TResponse>(
            TCommand comando,
            bool enqueue = false,
            CancellationToken cancellation = default
        )
            where TCommand : Command<TId, TResponse>
            where TResponse : notnull
            where TId : struct;

        /// <summary>
        /// Publicar fila.
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
