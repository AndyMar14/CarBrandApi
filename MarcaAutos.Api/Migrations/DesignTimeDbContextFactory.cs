using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MarcaAutos.Api.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Data.AppDbContext>
    {
        public Data.AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Data.AppDbContext>();
            // This connection string is only used for design-time operations
            // The actual connection string comes from configuration at runtime
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MarcasDb;Username=postgres;Password=postgres");

            return new Data.AppDbContext(optionsBuilder.Options);
        }
    }
}

