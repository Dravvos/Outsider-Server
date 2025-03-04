using Microsoft.EntityFrameworkCore;

namespace Outsider.ProdutoAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {

        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<TabelaGeralModel> TabelaGeral { get; set; }
        public DbSet<TabelaGeralItemModel> TabelaGeralItem { get; set; }

    }
}
