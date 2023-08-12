namespace Library.Tests.Common
{
    using global::Data.Repositories;

    using Library.Tests.Common.Interfaces;

    internal class PessoaRepository : Repository<PessoaContext, Pessoa>, IPessoaRepository
    {
        public PessoaRepository(PessoaContext context) : base(context) { }

        public string GetStringValue(string parametro)
        {
            throw new System.NotImplementedException();
        }
    }
}
