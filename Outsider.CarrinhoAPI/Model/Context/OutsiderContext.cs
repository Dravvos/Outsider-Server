using Microsoft.EntityFrameworkCore;
using Outsider.CarrinhoAPI.Model;

namespace Outsider.CarrinhoAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }
        public OutsiderContext()
        {
            
        }

        public DbSet<ItemCarrinhoModel> ItensCarrinho { get; set; }
        public DbSet<CarrinhoModel> Carrinho { get; set; }
        public DbSet<TabelaGeralModel> TabelaGeral { get; set; }
        public DbSet<TabelaGeralItemModel> TabelaGeralItem { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }
    }
}
