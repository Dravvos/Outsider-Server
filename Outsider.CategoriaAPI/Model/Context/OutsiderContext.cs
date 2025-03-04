using Microsoft.EntityFrameworkCore;

namespace Outsider.CategoriaAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {

        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<CategoriaModel> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaModel>().HasData(new CategoriaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Bermuda",
                DataInclusao = DateTime.Now,
            });
            modelBuilder.Entity<CategoriaModel>().HasData(new CategoriaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Meia",
                DataInclusao = DateTime.Now,
            });
            modelBuilder.Entity<CategoriaModel>().HasData(new CategoriaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Camiseta",
                DataInclusao = DateTime.Now,
            });
            modelBuilder.Entity<CategoriaModel>().HasData(new CategoriaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Tênis",
                DataInclusao = DateTime.Now,
            });
            modelBuilder.Entity<CategoriaModel>().HasData(new CategoriaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Camisa",
                DataInclusao = DateTime.Now,
            });
            modelBuilder.Entity<CategoriaModel>().HasData(new CategoriaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Calça",
                DataInclusao = DateTime.Now,
            });
        }
    }
}
