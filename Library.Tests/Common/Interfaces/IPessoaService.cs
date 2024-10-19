namespace Library.Tests.Common.Interfaces;

using Library.Tests.Common;

public interface IPessoaService : IService<Pessoa>
{
    string GetStringValue(string parametro);
}