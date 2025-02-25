// -----------------------------------------------------------------------
// <copyright file="Service.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Services;

using Core.Interfaces;


using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

public abstract class Service<TId, TEntity, TRepository>(
    TRepository repository
) : IService<TId, TEntity>
    where TId : notnull
    where TEntity : IEntity<TId>
    where TRepository : IRepository<TId, TEntity>
{
    protected readonly TRepository _repository = repository;

    public async Task CreateAsync(
        TEntity item
    ) => await CreateAsync(item, CancellationToken.None);

    public async Task CreateAsync(
        IEnumerable<TEntity> items
    ) => await CreateAsync(items, CancellationToken.None);

    public async Task DeleteAsync(
        TEntity item
    ) => await DeleteAsync(item, CancellationToken.None);

    public async Task UpdateAsync(
        TId id,
        TEntity item
    ) => await UpdateAsync(id, item, CancellationToken.None);

    public async Task<TEntity?> GetAsync(
        TId id
    ) => await GetAsync(id, CancellationToken.None);

    public async Task<IList<TEntity>> GetAllAsync(
        int amountToSkip = 0,
        int amountToTake = 25
    ) => await GetAllAsync(
        amountToSkip,
        amountToTake,
        CancellationToken.None
    );

    public async Task<IList<TEntity>> SearchAsync(
        Expression<Func<TEntity, bool>> predicate
    ) => await SearchAsync(predicate, CancellationToken.None);

    public async Task<TEntity?> GetAsync(
        TId id,
        CancellationToken cancellationToken
    ) => await _repository
        .GetAsync(id, cancellationToken);

    public async Task CreateAsync(
        TEntity item,
        CancellationToken cancellationToken
    ) => await _repository
        .CreateAsync(item, cancellationToken);

    public async Task CreateAsync(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken
    ) => await _repository
        .CreateAsync(items, cancellationToken);

    public async Task DeleteAsync(
        TEntity item,
        CancellationToken cancellationToken
    ) => await _repository
        .DeleteAsync(item);

    public async Task UpdateAsync(
        TId id,
        TEntity item,
        CancellationToken cancellationToken
    ) => await _repository
        .UpdateAsync(id, item);

    public async Task<IList<TEntity>> GetAllAsync(
        int amountToSkip,
        int amountToTake,
        CancellationToken cancellationToken
    ) => await _repository
        .GetAll(amountToSkip, amountToTake)
        .ToAsyncEnumerable()
        .ToListAsync(cancellationToken);

    public async Task<IList<TEntity>> SearchAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken
    ) => await _repository
        .Search(predicate)
        .ToAsyncEnumerable()
        .ToListAsync(cancellationToken);
}
