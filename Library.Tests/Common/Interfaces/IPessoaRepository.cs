namespace Library.Tests.Common.Interfaces
{
    using global::Core.Interfaces.Repositories;

    public interface IPessoaRepository : IRepository<Pessoa, long>
    {
        string GetStringValue(string parametro);
    }
}
