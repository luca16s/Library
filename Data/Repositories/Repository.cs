// -----------------------------------------------------------------------
// <copyright file="Repository.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
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

public class Repository<TContext, TEntity> : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : Entity
{
    private TContext Context { get; }
    private IUnitOfWork UnitOfWork { get; set; }
    protected DbSet<TEntity> DbSet { get; set; }

    public Repository(
        TContext context,
        IUnitOfWork unitOfWork
    )
    {
        Context = context;
        UnitOfWork = unitOfWork;
        DbSet = Context.Set<TEntity>();
    }

    public async Task Create(
        TEntity item,
        CancellationToken cancellationToken
    )
    {
        var transaction = await UnitOfWork.BeginTransactionAsync();

        try
        {
            EntityEntry<TEntity> entity = await DbSet.AddAsync(item, cancellationToken);
            entity.State = EntityState.Added;

            await UnitOfWork.CommitTransactionAsync(transaction);
        }
        catch (DbUpdateException e)
        {
            await UnitOfWork.RollbackTransactionAsync();
            throw new Exception(e.Message);
        }
    }

    public async Task Create(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken
    )
    {
        var transaction = await UnitOfWork.BeginTransactionAsync();

        try
        {
            foreach (var item in items)
            {
                EntityEntry<TEntity> entity = await DbSet.AddAsync(item, cancellationToken);
                entity.State = EntityState.Added;

                await UnitOfWork.CommitTransactionAsync(transaction);
            }
        }
        catch (DbUpdateException e)
        {
            await UnitOfWork.RollbackTransactionAsync();
            throw new Exception(e.Message);
        }
    }

    public async Task Delete(
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

    public async Task Update(
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
        catch (Exception e)
        {
            await UnitOfWork.RollbackTransactionAsync();
            throw new Exception(e.Message);
        }
    }

    public async Task<TEntity?> Get(
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
        return DbSet.AsQueryable()
            .Skip(amountToSkip)
            .Take(amountToTake);
    }

    public async Task<long> Count(
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

    public async Task<TResult> Max<TResult>(
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

    public async Task<TResult> Min<TResult>(
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
}
