namespace Data
{
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseContext<TContext> : DbContext
        where TContext : DbContext
    {
        public virtual required string DefaultSchema { get; set; }

        public BaseContext(
            DbContextOptions<TContext> options
        ) : base(options) { }
    }
}
