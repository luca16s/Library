namespace Library.Tests.Common
{
    using Microsoft.EntityFrameworkCore;

    public class PessoaContext : DbContext
    {
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder
                .ApplyConfiguration(new PessoaConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
