// -----------------------------------------------------------------------
// <copyright file="Repository.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Data.Repositories;

using Core.Interfaces.Repositories;
using Core.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

public abstract class Repository<TContext, TEntity>(
    TContext context,
    IUnitOfWork unitOfWork
) : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : Entity
{
    private TContext Context { get; } = context;
    private IUnitOfWork UnitOfWork { get; set; } = unitOfWork;
    protected DbSet<TEntity> DbSet { get; set; } = context.Set<TEntity>();

    public async Task CreateAsync(
        TEntity item
    ) => await CreateAsync(item, CancellationToken.None);

    public async Task CreateAsync(
        TEntity item,
        CancellationToken cancellationToken
    )
    {
        var transaction = await UnitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            EntityEntry<TEntity> entity = await DbSet.AddAsync(item, cancellationToken);
            entity.State = EntityState.Added;

            await UnitOfWork.CommitTransactionAsync(transaction, cancellationToken);
        }
        catch (DbUpdateException e)
        {
            await UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new Exception(e.Message);
        }
    }

    public async Task CreateAsync(
        IEnumerable<TEntity> items
    ) => await CreateAsync(items, CancellationToken.None);

    public async Task CreateAsync(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken
    )
    {
        var transaction = await UnitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            foreach (var item in items)
            {
                EntityEntry<TEntity> entity = await DbSet.AddAsync(item, cancellationToken);
                entity.State = EntityState.Added;

                await UnitOfWork.CommitTransactionAsync(transaction, cancellationToken);
            }
        }
        catch (DbUpdateException e)
        {
            await UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteAsync(
        TEntity item
    )
    {
        ArgumentNullException.ThrowIfNull(item);

        var transaction = await UnitOfWork.BeginTransactionAsync();

        try
        {
            EntityEntry<TEntity> entity = DbSet.Remove(item);
            entity.State = EntityState.Deleted;
            await UnitOfWork.CommitTransactionAsync(transaction);
        }
        catch (Exception e)
        {
            await UnitOfWork.RollbackTransactionAsync();
            throw new Exception(e.Message);
        }
    }

    public async Task UpdateAsync(
        long id,
        TEntity item
    )
    {
        ArgumentNullException.ThrowIfNull(item);

        var transaction = await UnitOfWork.BeginTransactionAsync();

        try
        {
            TEntity? entity = DbSet.Find(id) ??
                throw new NullReferenceException("Item pesquisado não existente no banco de dados.");

            Context.Entry(entity).State = EntityState.Detached;

            EntityEntry<TEntity> entry = DbSet.Update(item);
            entry.CurrentValues.SetValues(item);
            entry.State = EntityState.Modified;
            await UnitOfWork.CommitTransactionAsync(transaction);
        }
        catch
        {
            await UnitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<TEntity?> GetAsync(
        long id
    ) => await GetAsync(id, CancellationToken.None);

    public async Task<TEntity?> GetAsync(
        long id,
        CancellationToken cancellationToken
    )
    {
        return await DbSet.FindAsync(
            [id, cancellationToken],
            cancellationToken: cancellationToken
        );
    }

    public IQueryable<TEntity> GetAll(
        int amountToSkip = 0,
        int amountToTake = 25
    )
    {
        return DbSet
            .AsQueryable()
            .AsNoTracking()
            .Skip(amountToSkip)
            .Take(amountToTake);
    }

    public async Task<long> CountAsync()
        => await CountAsync(CancellationToken.None);

    public async Task<long> CountAsync(
        CancellationToken cancellationToken
    )
    {
        return await DbSet.CountAsync(cancellationToken);
    }

    public IQueryable<TEntity> Search(
        Expression<Func<TEntity, bool>> predicate
    )
    {
        return DbSet.Where(predicate);
    }

    public async Task<TResult> MinAsync<TResult>(
        Expression<Func<TEntity, TResult>> predicate
    ) => await MinAsync(predicate, CancellationToken.None);

    public async Task<TResult> MinAsync<TResult>(
        Expression<Func<TEntity, TResult>> predicate,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await DbSet.MinAsync(predicate, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.",
                ex
             );
        }
    }

    public async Task<TResult> MaxAsync<TResult>(
        Expression<Func<TEntity, TResult>> predicate
    ) => await MaxAsync(predicate, CancellationToken.None);

    public async Task<TResult> MaxAsync<TResult>(
        Expression<Func<TEntity, TResult>> predicate,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await DbSet.MaxAsync(predicate, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.",
                ex
             );
        }
    }
}
