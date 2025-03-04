using Microsoft.EntityFrameworkCore;

namespace Outsider.TabelaGeralAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {

        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<TabelaGeralItemModel> TabelaGeralItem { get; set; }
        public DbSet<TabelaGeralModel> TabelasGerais { get; set; }
    }
}
