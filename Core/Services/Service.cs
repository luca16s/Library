// -----------------------------------------------------------------------
// <copyright file="Service.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Services;

using Core.Interfaces;

using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public abstract class Service<TRepository, TId, TEntity>(
    IUnitOfWork unitOfWork,
    TRepository repository
    ) : IService<TId, TEntity>
    where TId : notnull
    where TEntity : IEntity<TId>
    where TRepository : IRepository<TId, TEntity>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly TRepository _repository = repository;

    public async Task CreateAsync(
        TEntity item
    ) => await CreateAsync(item, CancellationToken.None);

    public async Task CreateAsync(
        TEntity item,
        CancellationToken cancellationToken
    )
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _repository.CreateAsync(item, cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new InvalidOperationException("");
        }

        await _unitOfWork.CommitAsync(transaction, cancellationToken);
    }

    public async Task CreateAsync(
        IEnumerable<TEntity> items
    ) => await CreateAsync(items, CancellationToken.None);

    public async Task CreateAsync(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken
    )
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _repository.CreateAsync(items, cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new InvalidOperationException("");
        }

        await _unitOfWork.CommitAsync(transaction, cancellationToken);
    }

    public async Task Delete(
        TEntity item
    )
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync();
        await _repository.DeleteAsync(item);

        await _unitOfWork.CommitAsync(transaction);
    }

    public async Task Update(
        TId id,
        TEntity item
    )
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _repository.UpdateAsync(id, item);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new InvalidOperationException("");
        }

        await _unitOfWork.CommitAsync(transaction);
    }

    public async Task<TEntity?> GetAsync(
        TId id
    ) => await GetAsync(id, CancellationToken.None);

    public async Task<TEntity?> GetAsync(
        TId id,
        CancellationToken cancellationToken
    ) => await _repository.GetAsync(id, cancellationToken);

    public IQueryable<TEntity> GetAll(
        int amountToSkip = 0,
        int amountToTake = 25
    ) => _repository.GetAll(amountToSkip, amountToTake);

    public IQueryable<TEntity> Search(
        Expression<Func<TEntity, bool>> predicate
    ) => _repository.Search(predicate);
}
