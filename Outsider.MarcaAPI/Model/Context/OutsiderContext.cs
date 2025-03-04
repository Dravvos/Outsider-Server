using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static Outsider.MarcaAPI.Model.MarcaModel;

namespace Outsider.MarcaAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<MarcaModel> Marcas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaModel>().ToTable(t=>t.ExcludeFromMigrations());
        }


    }
}
