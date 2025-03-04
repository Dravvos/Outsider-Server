using Microsoft.EntityFrameworkCore;

namespace Outsider.Email.Model.Context
{
    public class OutsiderContext : DbContext
    {

        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<EmailLog> Logs { get; set; }

    }
}
