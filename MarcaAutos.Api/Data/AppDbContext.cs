

using MarcaAutos.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarcaAutos.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MarcaAuto> MarcasAutos => Set<MarcaAuto>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MarcaAuto>(entity =>
            {
                entity.ToTable("MarcasAutos");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // Data Seed: al menos 3 marcas
            modelBuilder.Entity<MarcaAuto>().HasData(
                new MarcaAuto { Id = 1, Nombre = "Toyota" },
                new MarcaAuto { Id = 2, Nombre = "Honda" },
                new MarcaAuto { Id = 3, Nombre = "Ford" }
            );
        }
    }
}
