// -----------------------------------------------------------------------
// <copyright file="Repository.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Data.Repositories
{
    using Core.Interfaces.Repositories;
    using Core.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<TContext, TEntity, TId> : IRepository<TEntity, TId>
        where TContext : DbContext
        where TEntity : Entity<TId>
        where TId : struct
    {
        private TContext Context { get; }

        protected DbSet<TEntity> DbSet { get; set; }

        public Repository(TContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task Create(TEntity item)
        {
            try
            {
                EntityEntry<TEntity> entity = await DbSet.AddAsync(item);
                entity.State = EntityState.Added;
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task Create(IEnumerable<TEntity> items)
        {
            try
            {
                foreach (var item in items)
                {
                    EntityEntry<TEntity> entity = await DbSet.AddAsync(item);
                    entity.State = EntityState.Added;
                }
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task Delete(TEntity item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            EntityEntry<TEntity> entity = DbSet.Remove(item);
            entity.State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public Task Update(TId id, TEntity item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            TEntity? entity = DbSet.Find(id) ??
                throw new NullReferenceException("Item pesquisado não existente no banco de dados.");

            Context.Entry(entity).State = EntityState.Detached;

            EntityEntry<TEntity> entry = DbSet.Update(item);
            entry.CurrentValues.SetValues(item);
            entry.State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public async Task<TEntity?> Get(TId id)
        {
            return await DbSet.FindAsync(id);
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

        public async Task<long> Count()
        {
            return await DbSet.CountAsync();
        }

        public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public async Task<TResult> Max<TResult>(Expression<Func<TEntity, TResult>> predicate)
        {
            try
            {
                return await DbSet.MaxAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.",
                    ex
                 );
            }
        }

        public async Task<TResult> Min<TResult>(Expression<Func<TEntity, TResult>> predicate)
        {
            try
            {
                return await DbSet.MinAsync(predicate);
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
}
