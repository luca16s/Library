namespace Library.Tests.Common.Interfaces
{
    using global::Core.Interfaces.Services;

    using Library.Tests.Common;

    public interface IPessoaService : IService<Pessoa, long>
    {
        string GetStringValue(string parametro);
    }
}