// -----------------------------------------------------------------------
// <copyright file="Service.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
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

public class Service<TRepository, TEntity>(
    IUnitOfWork unitOfWork,
    TRepository repository
    ) : IService<TEntity>
    where TEntity : Entity
    where TRepository : IRepository<TEntity>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly TRepository _repository = repository;

    public async Task Create(TEntity item)
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransaction();
        try
        {
            await _repository.Create(item);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            throw new InvalidOperationException("");
        }

        await _unitOfWork.CommitTransaction(transaction);
    }

    public async Task Create(IEnumerable<TEntity> items)
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransaction();
        try
        {
            await _repository.Create(items);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            throw new InvalidOperationException("");
        }

        await _unitOfWork.CommitTransaction(transaction);
    }

    public async Task Delete(TEntity item)
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransaction();
        await _repository.Delete(item);

        await _unitOfWork.CommitTransaction(transaction);
    }

    public async Task Update(long id, TEntity item)
    {
        using IDbContextTransaction transaction = await _unitOfWork.BeginTransaction();
        try
        {
            await _repository.Update(id, item);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransaction();
            throw new InvalidOperationException("");
        }

        await _unitOfWork.CommitTransaction(transaction);
    }

    public async Task<TEntity?> Get(long id)
    {
        return await _repository.Get(id);
    }

    public IQueryable<TEntity> GetAll(int amountToSkip = 0, int amountToTake = 25)
    {
        return _repository.GetAll(amountToSkip, amountToTake);
    }

    public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
    {
        return _repository.Search(predicate);
    }
}
