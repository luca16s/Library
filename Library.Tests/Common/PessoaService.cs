namespace Library.Tests.Common;

using Library.Tests.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

public class PessoaService(
    IPessoaRepository repositorio
    ) : IPessoaService
{
    private readonly IPessoaRepository Repositorio = repositorio;

    public async Task CreateAsync(
        Pessoa item
    ) => await CreateAsync(item, CancellationToken.None);

    public Task CreateAsync(
        Pessoa item,
        CancellationToken cancellationToken
    )
    {
        return Repositorio.CreateAsync(item, CancellationToken.None);
    }

    public Task CreateAsync(
        IEnumerable<Pessoa> items
    ) => CreateAsync(items, CancellationToken.None);

    public Task CreateAsync(
        IEnumerable<Pessoa> items,
        CancellationToken cancellationToken
    )
    {
        return Repositorio.CreateAsync(items, CancellationToken.None);
    }

    public Task Delete(
        Pessoa item
    )
    {
        return Repositorio.DeleteAsync(item);
    }

    public Task<Pessoa> GetAsync(
        long id
    ) => GetAsync(id, CancellationToken.None);

    public Task<Pessoa> GetAsync(
        long id,
        CancellationToken cancellationToken
    )
    {
        return Repositorio.GetAsync(id, CancellationToken.None);
    }

    public IQueryable<Pessoa> GetAll(
        int amountToSkip = 25,
        int amountToTake = 25
    )
    {
        return Repositorio.GetAll(amountToSkip, amountToTake);
    }

    public IQueryable<Pessoa> Search(
        Expression<Func<Pessoa, bool>> predicate
    )
    {
        return Repositorio.Search(predicate);
    }

    public Task Update(
        long id,
        Pessoa item
    )
    {
        return Repositorio.UpdateAsync(id, item);
    }

    public string GetStringValue(
        string parametro
    )
    {
        return Repositorio.GetStringValue(parametro);
    }
}