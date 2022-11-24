// -----------------------------------------------------------------------
// <copyright file="IService.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces.Services
{
    using Core.Models;

    using System.Linq.Expressions;

    /// <summary>
    /// Interface assíncrona de serviço.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Entidade que será salva.
    /// </typeparam>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public interface IService<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        /// <summary>
        /// Adiciona nova entidade no banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="item">
        /// Entidade a ser salva.
        /// </param>
        Task Create(TEntity item);

        /// <summary>
        /// Adiciona de forma assíncrona diversos items ao banco de dados.
        /// </summary>
        /// <param name="items">
        /// Entidades para serem salvas.
        /// </param>
        /// <returns></returns>
        Task Create(IEnumerable<TEntity> items);

        /// <summary>
        /// Deleta uma entidade no banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="item">
        /// Entidade a ser deletada.
        /// </param>
        Task Delete(TEntity item);

        /// <summary>
        /// Retorna todas as entidades do banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="amount">
        /// Quantidade de itens a ser buscadas.
        /// </param>
        /// <returns>
        /// Todas as entidades.
        /// </returns>
        IQueryable<TEntity> GetAll(int amount);

        /// <summary>
        /// Retorna uma entidade com base em um identificador de forma assíncrona.
        /// </summary>
        /// <param name="id">
        /// Identificador da entidade.
        /// </param>
        /// <returns>
        /// Entidade encontrada.
        /// </returns>
        Task<TEntity?> Get(TId id);

        /// <summary>
        /// Atualiza uma entidade com base em um identificador passado de forma assíncrona.
        /// </summary>
        /// <param name="id">
        /// Identificador da entidade.
        /// </param>
        /// <param name="item">
        /// Entidade a ser atualizada.
        /// </param>
        Task Update(TId id, TEntity item);

        /// <summary>
        /// Busca determinados itens na base de dados.
        /// </summary>
        /// <param name="predicate">
        /// Termo de busca.
        /// </param>
        /// <returns>
        /// Lista de itens encontrados.
        /// </returns>
        IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
    }
}
