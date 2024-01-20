// -----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces.Repositories;

using Microsoft.EntityFrameworkCore.Storage;

/// <summary>
/// Classe para servir de interface no salvamento do banco de dados.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Inicia transação com o banco de dados.
    /// </summary>
    /// <returns>
    /// Retorna a transação.
    /// </returns>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    /// Inicia transação com o banco de dados.
    /// </summary>
    /// <param name="cancellationToken">
    /// Um <see cref="CancellationToken" /> para observar enquanto espera a conclusão da tarefa.
    /// </param>
    /// <returns>
    /// Retorna a transação.
    /// </returns>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Comita a transação do banco.
    /// </summary>
    /// <param name="transaction">
    /// Transação aberta.
    /// </param>
    /// <returns>
    /// Retorna a task.
    /// </returns>
    Task CommitTransactionAsync(IDbContextTransaction transaction);

    /// <summary>
    /// Comita a transação do banco.
    /// </summary>
    /// <param name="transaction">
    /// Transação aberta.
    /// </param>
    /// <param name="cancellationToken">
    /// Um <see cref="CancellationToken" /> para observar enquanto espera a conclusão da tarefa.
    /// </param>
    /// <returns>
    /// Retorna a task.
    /// </returns>
    Task CommitTransactionAsync(
        IDbContextTransaction transaction,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Reverte alterações.
    /// </summary>
    Task RollbackTransactionAsync();

    /// <summary>
    /// Reverte alterações.
    /// </summary>
    /// <param name="cancellationToken">
    /// Um <see cref="CancellationToken" /> para observar enquanto espera a conclusão da tarefa.
    /// </param>
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
