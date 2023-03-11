namespace Library.Tests.Common
{
    using global::Core.Models;

    public class Pessoa : Entity<long>
    {
        public Pessoa() : base(default) { }

        public Pessoa(long id) : base(id) { }

        public int Idade { get; set; } = 0;

        public string Nome { get; set; } = string.Empty;
    }
}