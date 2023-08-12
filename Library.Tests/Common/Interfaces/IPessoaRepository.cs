namespace Library.Tests.Common.Interfaces
{
    using global::Core.Interfaces.Repositories;

    public interface IPessoaRepository : IRepository<Pessoa>
    {
        string GetStringValue(string parametro);
    }
}
