// -----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces.Repositories
{
    using Core.Models;

    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface assíncrona para salvamento no banco de dados.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Entidade que será salva.
    /// </typeparam>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public interface IRepository<TEntity, TId>
        where TId : struct
        where TEntity : Entity<TId>
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
        /// Deleta uma entidade no banco de dados.
        /// </summary>
        /// <param name="item">
        /// Entidade a ser deletada.
        /// </param>
        Task Delete(TEntity item);

        /// <summary>
        /// Atualiza uma entidade com base em um identificador passado.
        /// </summary>
        /// <param name="id">
        /// Identificador da entidade.
        /// </param>
        /// <param name="item">
        /// Entidade a ser atualizada.
        /// </param>
        Task Update(TId id, TEntity item);

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
        /// Retorna todas as entidades do banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="amountToSkip">
        /// Quantidade de itens a serem ignorados.
        /// </param>
        /// <param name="amountToTake">
        /// Quantidade de itens a ser buscadas.
        /// </param>
        /// <returns>
        /// Todas as entidades.
        /// </returns>
        IQueryable<TEntity> GetAll(int amountToSkip = 0, int amountToTake = 25);

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

        /// <summary>
        /// Realiza a contagem de items salvos para determinada tabela.
        /// </summary>
        /// <returns>
        /// Retorna total de items salvos no banco de dados.
        /// </returns>
        Task<long> Count();

        /// <summary>
        /// Realiza a busca do maior item salvo no banco de dados.
        /// </summary>
        /// <typeparam name="TResult">
        /// Tipo do resultado da operação
        /// </typeparam>
        /// <param name="predicate">
        /// Termo de busca.
        /// </param>
        /// <returns>
        /// Retorna maior item encontrado.
        /// </returns>
        Task<TResult> Max<TResult>(Expression<Func<TEntity, TResult>> predicate);

        /// <summary>
        /// Realiza a busca do menor item salvo no banco de dados.
        /// </summary>
        /// <typeparam name="TResult">
        /// Tipo do resultado da operação
        /// </typeparam>
        /// <param name="predicate">
        /// Termo de busca.
        /// </param>
        /// <returns>
        /// Retorna menor item encontrado.
        /// </returns>
        Task<TResult> Min<TResult>(Expression<Func<TEntity, TResult>> predicate);
    }
}
