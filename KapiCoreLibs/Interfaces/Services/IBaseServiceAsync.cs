// -----------------------------------------------------------------------
// <copyright file="IBaseServiceAsync.cs" company="Îakaré Software'oka">
//     Copyright (c) Îakaré Software'oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
using KapiCoreLib.Models;

namespace KapiCoreLib.Interfaces.Services
{
    /// <summary>
    /// Interface assíncrona de serviço.
    /// </summary>
    /// <typeparam name="T">
    /// Entidade que será salva.
    /// </typeparam>
    public interface IBaseServiceAsync<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Adiciona nova entidade no banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="item">
        /// Entidade a ser salva.
        /// </param>
        /// <returns>
        /// Entidade salva.
        /// </returns>
        Task<T> AddItemAsync(T item);

        /// <summary>
        /// Deleta uma entidade no banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="item">
        /// Entidade a ser deletada.
        /// </param>
        void DeleteItem(T item);

        /// <summary>
        /// Retorna todas as entidades do banco de dados de forma assíncrona.
        /// </summary>
        /// <returns>
        /// Todas as entidades.
        /// </returns>
        Task<IEnumerable<T>> GetAllItemsAsync();

        /// <summary>
        /// Retorna uma entidade com base em um identificador de forma assíncrona.
        /// </summary>
        /// <param name="id">
        /// Identificador da entidade.
        /// </param>
        /// <returns>
        /// Entidade encontrada.
        /// </returns>
        Task<T> GetItemAsync(Guid id);

        /// <summary>
        /// Atualiza uma entidade com base em um identificador passado de forma assíncrona.
        /// </summary>
        /// <param name="id">
        /// Identificador da entidade.
        /// </param>
        /// <param name="item">
        /// Entidade a ser atualizada.
        /// </param>
        /// <returns>
        /// Entidade atualizada.
        /// </returns>
        Task<T> UpdateItem(Guid id, T item);
    }
}
