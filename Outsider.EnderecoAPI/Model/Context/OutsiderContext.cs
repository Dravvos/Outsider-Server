using Microsoft.EntityFrameworkCore;

namespace Outside.EnderecoAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {
        
        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<EnderecoModel> Enderecos { get; set; }
    }
}
