// -----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Data;

/// <summary>
/// Classe com comportamentos padrões da interface de UnitOfWork.
/// </summary>
/// <typeparam name="TContext">
/// Contexto do banco de dados a ser utilizado.
/// </typeparam>
/// <remarks>
/// Inicializa uma nova instância da classe de UnitOfWork.
/// </remarks>
/// <param name="baseContext">
/// Contexto a ser Manipulado.
/// </param>
public class UnitOfWork<TContext>(
    TContext baseContext
    ) where TContext : DbContext
{
    private readonly TContext _baseContext = baseContext;

    /// <summary>
    /// Retorna a transação atual.
    /// </summary>
    public Task<IDbContextTransaction> CurrentTransaction { get; private set; } = null!;

    /// <summary>
    /// Inicia transação com o banco de dados.
    /// </summary>
    /// <returns>
    /// Retorna a transação.
    /// </returns>
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (CurrentTransaction is not null)
            return await CurrentTransaction;

        _baseContext.Database.OpenConnection();

        CurrentTransaction = _baseContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return await CurrentTransaction;
    }

    /// <summary>
    /// Comita a transação do banco.
    /// </summary>
    /// <param name="transaction">
    /// Transação aberta.
    /// </param>
    /// <returns>
    /// Retorna a task.
    /// </returns>
    public async Task CommitTransactionAsync(
        IDbContextTransaction transaction
    )
    {
        ArgumentNullException.ThrowIfNull(transaction);

        if (await CurrentTransaction is IDbContextTransaction current)
        {
            if (transaction != current)
                throw new InvalidOperationException($"Transação {transaction.TransactionId} não é a atual.");
        }

        try
        {
            _baseContext.ChangeTracker.DetectChanges();
            bool isObjectSavedAsync = _baseContext.SaveChanges() > 0;

            if (isObjectSavedAsync)
                transaction.Commit();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (EntityEntry? entry in ex.Entries)
            {
                PropertyValues? proposedValues = entry.CurrentValues;
                PropertyValues? databaseValues = entry.GetDatabaseValues();

                foreach (IProperty? property in proposedValues.Properties)
                {
                    object? proposedValue = proposedValues[property];
                    object? databaseValue = databaseValues?[property];

                    throw new Exception($"Erro de concorrência ao salvar entidade: Atual: ({proposedValue}) ~ Banco: ({databaseValue})");
                }

                entry.OriginalValues.SetValues(proposedValues);
            }
        }
        catch (Exception ex)
        {
            foreach (EntityEntry entry in _baseContext.ChangeTracker.Entries().Where(e => e?.State != EntityState.Unchanged))
            {
                foreach (IProperty prop in entry.CurrentValues.Properties)
                {
                    object? val = prop?.PropertyInfo?.GetValue(entry?.Entity);
                    throw new Exception($"Erro ao salvar entidade: {prop}");
                }
            }

            await RollbackTransactionAsync();
            throw new DbUpdateException(ex.Message);
        }
    }

    /// <summary>
    /// Reverte alterações.
    /// </summary>
    public async Task RollbackTransactionAsync()
    {
        if (CurrentTransaction is null)
            throw new InvalidOperationException("Não é possível dar rollback sem uma transação aberta.");

        await (await CurrentTransaction).RollbackAsync();
        CurrentTransaction = null!;
    }
}
