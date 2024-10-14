// -----------------------------------------------------------------------
// <copyright file="IService.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces;

using System.Linq.Expressions;

/// <summary>
/// Interface assíncrona de serviço.
/// </summary>
/// <typeparam name="TId">
/// Tipo da entidade a ser salva.
/// </typeparam>
/// <typeparam name="TEntity">
/// Entidade que será salva.
/// </typeparam>
public interface IService<TId, TEntity>
    where TId : notnull
    where TEntity : IEntity<TId>
{
    /// <summary>
    /// Adiciona nova entidade no banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="item">
    /// Entidade a ser salva.
    /// </param>
    Task CreateAsync(
        TEntity item
    );

    /// <summary>
    /// Adiciona nova entidade no banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="item">
    /// Entidade a ser salva.
    /// </param>
    /// <param name="cancellationToken">
    /// Um <see cref="CancellationToken" /> para observar enquanto espera a conclusão da tarefa.
    /// </param>
    Task CreateAsync(
        TEntity item,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Adiciona de forma assíncrona diversos items ao banco de dados.
    /// </summary>
    /// <param name="items">
    /// Entidades para serem salvas.
    /// </param>
    /// <returns></returns>
    Task CreateAsync(
        IEnumerable<TEntity> items
    );

    /// <summary>
    /// Adiciona de forma assíncrona diversos items ao banco de dados.
    /// </summary>
    /// <param name="items">
    /// Entidades para serem salvas.
    /// </param>
    /// <param name="cancellationToken">
    /// Um <see cref="CancellationToken" /> para observar enquanto espera a conclusão da tarefa.
    /// </param>
    /// <returns></returns>
    Task CreateAsync(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Deleta uma entidade no banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="item">
    /// Entidade a ser deletada.
    /// </param>
    Task Delete(
        TEntity item
    );

    /// <summary>
    /// Atualiza uma entidade com base em um identificador passado de forma assíncrona.
    /// </summary>
    /// <param name="id">
    /// Identificador da entidade.
    /// </param>
    /// <param name="item">
    /// Entidade a ser atualizada.
    /// </param>
    Task Update(
        TId id,
        TEntity item
    );

    /// <summary>
    /// Retorna uma entidade com base em um identificador de forma assíncrona.
    /// </summary>
    /// <param name="id">
    /// Identificador da entidade.
    /// </param>
    /// <returns>
    /// Entidade encontrada.
    /// </returns>
    Task<TEntity?> GetAsync(
        TId id
    );

    /// <summary>
    /// Retorna uma entidade com base em um identificador de forma assíncrona.
    /// </summary>
    /// <param name="id">
    /// Identificador da entidade.
    /// </param>
    /// <param name="cancellationToken">
    /// Um <see cref="CancellationToken" /> para observar enquanto espera a conclusão da tarefa.
    /// </param>
    /// <returns>
    /// Entidade encontrada.
    /// </returns>
    Task<TEntity?> GetAsync(
        TId id,
        CancellationToken cancellationToken
    );

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
    IQueryable<TEntity> GetAll(
        int amountToSkip = 0,
        int amountToTake = 25
    );

    /// <summary>
    /// Busca determinados itens na base de dados.
    /// </summary>
    /// <param name="predicate">
    /// Termo de busca.
    /// </param>
    /// <returns>
    /// Lista de itens encontrados.
    /// </returns>
    IQueryable<TEntity> Search(
        Expression<Func<TEntity, bool>> predicate
    );
}
