using Microsoft.EntityFrameworkCore;

namespace Outside.ClienteAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {
        
        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<ClienteModel> Clientes { get; set; }
    }
}
