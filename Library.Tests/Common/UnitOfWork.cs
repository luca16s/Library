namespace Library.Tests.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System.Data;
using System.Threading;
using System.Threading.Tasks;

public class UnitOfWork(PessoaContext pessoaContext) : IUnitOfWork
{
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await BeginTransactionAsync(CancellationToken.None);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await pessoaContext.Database.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken
        );
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        _ = await Task.FromResult(0);
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        _ = await Task.FromResult(0);
    }

    public async Task RollbackTransactionAsync()
    {
        _ = await Task.FromResult(0);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        _ = await Task.FromResult(0);
    }
}
