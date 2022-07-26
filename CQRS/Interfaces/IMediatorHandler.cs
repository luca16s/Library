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
    using CQRS.Commands;
    using CQRS.Events;

    using System.Threading.Tasks;

    /// <summary>
    /// Interface do manipulador de mediação.
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// Lança o evento.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo do Evento.
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
        /// <returns>
        /// Task
        /// </returns>
        Task RaiseEvent<T>(T evento,
                           bool enqueue = false,
                           CancellationToken cancellation = default) where T : Event;

        /// <summary>
        /// Envia o comando.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo do Evento.
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
        /// <returns>
        /// Task
        /// </returns>
        Task SendCommand<T>(T comando,
                            bool enqueue = false,
                            CancellationToken cancellation = default) where T : Command;

        /// <summary>
        /// Publicar fila.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo do comando.
        /// </typeparam>
        /// <param name="comando">
        /// Comando a ser publicado.
        /// </param>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        /// <returns>
        /// Task
        /// </returns>
        Task PublishQueue<T>(T comando, CancellationToken cancellation = default);
    }
}
