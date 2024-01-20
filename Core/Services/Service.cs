// -----------------------------------------------------------------------
// <copyright file="Service.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Services;

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;

using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public abstract class Service<TRepository, TEntity>(
    IUnitOfWork unitOfWork,
    TRepository repository
    ) : IService<TEntity>
    where TEntity : Entity
    where TRepository : IRepository<TEntity>
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

        await _unitOfWork.CommitTransactionAsync(transaction, cancellationToken);
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

        await _unitOfWork.CommitTransactionAsync(transaction, cancellationToken);
    }

    public async Task Delete(
        TEntity item
    )
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync();
        await _repository.DeleteAsync(item);

        await _unitOfWork.CommitTransactionAsync(transaction);
    }

    public async Task Update(
        long id,
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

        await _unitOfWork.CommitTransactionAsync(transaction);
    }

    public async Task<TEntity?> GetAsync(
        long id
    ) => await GetAsync(id, CancellationToken.None);

    public async Task<TEntity?> GetAsync(
        long id,
        CancellationToken cancellationToken
    )
    {
        return await _repository.GetAsync(id, cancellationToken);
    }

    public IQueryable<TEntity> GetAll(
        int amountToSkip = 0,
        int amountToTake = 25
    )
    {
        return _repository.GetAll(amountToSkip, amountToTake);
    }

    public IQueryable<TEntity> Search(
        Expression<Func<TEntity, bool>> predicate
    )
    {
        return _repository.Search(predicate);
    }
}
