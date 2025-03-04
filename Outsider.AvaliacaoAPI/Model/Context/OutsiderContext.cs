using Microsoft.EntityFrameworkCore;

namespace Outsider.AvaliacaoAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {
        public OutsiderContext()
        {
            
        }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<AvaliacaoModel> Avaliacoes { get; set; }
        public DbSet<TabelaGeralModel> TabelaGeral { get; set; }
        public DbSet<TabelaGeralItemModel> TabelaGeralItem { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }
    }
}
