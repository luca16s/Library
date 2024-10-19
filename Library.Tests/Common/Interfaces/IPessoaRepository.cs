namespace Library.Tests.Common.Interfaces;
public interface IPessoaRepository : IRepository<Pessoa>
{
    string GetStringValue(string parametro);
}
