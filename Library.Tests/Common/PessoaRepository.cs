namespace Library.Tests.Common;

using global::Core.Interfaces.Repositories;
using global::Data.Repositories;

using Library.Tests.Common.Interfaces;

internal class PessoaRepository(
    IUnitOfWork unitOfWork,
    PessoaContext context
) : Repository<PessoaContext, Pessoa>(context, unitOfWork), IPessoaRepository
{
    public string GetStringValue(string parametro)
    {
        throw new System.NotImplementedException();
    }
}
