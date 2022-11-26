namespace Library.Tests.Common
{
    using Library.Tests.Common.Interfaces;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository Repositorio;

        public PessoaService(
            IPessoaRepository repositorio
        )
        {
            Repositorio = repositorio;
        }

        public Task Create(Pessoa item)
        {
            return Repositorio.Create(item);
        }

        public Task Create(IEnumerable<Pessoa> items)
        {
            return Repositorio.Create(items);
        }

        public Task Delete(Pessoa item)
        {
            return Repositorio.Delete(item);
        }

        public Task<Pessoa> Get(long id)
        {
            return Repositorio.Get(id);
        }

        public IQueryable<Pessoa> GetAll(int amountToSkip = 25, int amountToTake = 25)
        {
            return Repositorio.GetAll(amountToSkip, amountToTake);
        }

        public string GetStringValue(string parametro)
        {
            return Repositorio.GetStringValue(parametro);
        }

        public IQueryable<Pessoa> Search(Expression<Func<Pessoa, bool>> predicate)
        {
            return Repositorio.Search(predicate);
        }

        public Task Update(long id, Pessoa item)
        {
            return Repositorio.Update(id, item);
        }
    }
}