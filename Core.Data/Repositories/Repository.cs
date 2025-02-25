// -----------------------------------------------------------------------
// <copyright file="Repository.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Data.Repositories;

using Core.Interfaces;
using Core.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

public abstract class Repository<TId, TEntity, TContext>(
    TContext context,
    IUnitOfWork unitOfWork
) : IRepository<TId, TEntity>
    where TId : notnull
    where TContext : DbContext
    where TEntity : Entity<TId>
{
    private TContext Context { get; } = context;
    private IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
    protected DbSet<TEntity> DbSet { get; set; } = context.Set<TEntity>();

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

    public async Task<TEntity?> GetManyAsync(
        TId[] ids
    ) => await GetManyAsync(ids, CancellationToken.None);

    public async Task<long> CountAsync(
    ) => await CountAsync(CancellationToken.None);

    public async Task<TResult> MinAsync<TResult>(
        Expression<Func<TEntity, TResult>> predicate
    ) => await MinAsync(predicate, CancellationToken.None);

    public async Task<TResult> MaxAsync<TResult>(
        Expression<Func<TEntity, TResult>> predicate
    ) => await MaxAsync(predicate, CancellationToken.None);

    public async Task CreateAsync(
        TEntity item,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var transaction = await UnitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            EntityEntry<TEntity> entity = await DbSet.AddAsync(item, cancellationToken);

            if (entity is not null)
                entity.State = EntityState.Added;

            await UnitOfWork.CommitAsync(transaction, cancellationToken);
        }
        catch
        {
            await UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task CreateAsync(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        var transaction = await UnitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await DbSet.AddRangeAsync(items, cancellationToken);
            await UnitOfWork.CommitAsync(transaction, cancellationToken);
        }
        catch
        {
            await UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task DeleteAsync(
        TEntity item,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(item);
        cancellationToken.ThrowIfCancellationRequested();

        var transaction = await UnitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            EntityEntry<TEntity> entity = DbSet.Remove(item);

            if (entity is not null)
                entity.State = EntityState.Deleted;

            await UnitOfWork.CommitAsync(transaction, cancellationToken);
        }
        catch
        {
            await UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdateAsync(
        TId id,
        TEntity item,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(item);
        cancellationToken.ThrowIfCancellationRequested();

        var transaction = await UnitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            TEntity? entity = DbSet.Find(id) ??
                throw new NullReferenceException("Item pesquisado não existente no banco de dados.");

            if (Context.Entry(entity) is not null)
                Context.Entry(entity).State = EntityState.Detached;

            EntityEntry<TEntity> entry = DbSet.Update(item);

            if (entry is not null)
            {
                entry.CurrentValues.SetValues(item);
                entry.State = EntityState.Modified;
            }

            await UnitOfWork.CommitAsync(transaction, cancellationToken);
        }
        catch
        {
            await UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<TEntity?> GetAsync(
        TId id,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await DbSet.FindAsync(
                [id, cancellationToken],
                cancellationToken
            );
        }
        catch
        {
            throw;
        }
    }

    public async Task<TEntity?> GetManyAsync(
        TId[] ids,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await DbSet.FindAsync(
                [ids, cancellationToken],
                cancellationToken
            );
        }
        catch
        {
            throw;
        }
    }

    public IQueryable<TEntity> GetAll(
        int amountToSkip = 0,
        int amountToTake = 25
    ) => DbSet
            .AsQueryable()
            .AsNoTracking()
            .Skip(amountToSkip)
            .Take(amountToTake);

    public IQueryable<TEntity> Search(
        Expression<Func<TEntity, bool>> predicate
    )
    {
        try
        {
            return DbSet
                .AsQueryable()
                .AsNoTracking()
                .Where(predicate);
        }
        catch
        {
            throw;
        }
    }

    public async Task<long> CountAsync(
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await DbSet.CountAsync(cancellationToken);
        }
        catch
        {
            throw;
        }
    }

    public async Task<TResult> MinAsync<TResult>(
        Expression<Func<TEntity, TResult>> predicate,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await DbSet.MinAsync(predicate, cancellationToken);
        }
        catch
        {
            throw;
        }
    }

    public async Task<TResult> MaxAsync<TResult>(
        Expression<Func<TEntity, TResult>> predicate,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await DbSet.MaxAsync(predicate, cancellationToken);
        }
        catch
        {
            throw;
        }
    }
}
