namespace Library.Tests.Common;

using Microsoft.EntityFrameworkCore;

public class PessoaContext(DbContextOptions<PessoaContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        _ = builder
            .ApplyConfiguration(new PessoaConfiguration());

        base.OnModelCreating(builder);
    }

    public DbSet<Pessoa> Pessoas { get; set; }
}
