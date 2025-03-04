using Microsoft.EntityFrameworkCore;
using Outside.PedidoAPI.Model;

namespace Outsider.PedidoAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {

        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<PedidoItemModel> PedidoItens { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<TabelaGeralModel> TabelaGeral { get; set; }
        public DbSet<TabelaGeralItemModel> TabelaGeralItem { get; set; }
        public DbSet<EnderecoModel> Endereco { get; set; }
    }
}
