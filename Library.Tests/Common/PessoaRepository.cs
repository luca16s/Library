namespace Library.Tests.Common;

using global::Data.Repositories;

using Library.Tests.Common.Interfaces;

internal class PessoaRepository(PessoaContext context) : Repository<PessoaContext, Pessoa>(context), IPessoaRepository
{
    public string GetStringValue(string parametro)
    {
        throw new System.NotImplementedException();
    }
}
