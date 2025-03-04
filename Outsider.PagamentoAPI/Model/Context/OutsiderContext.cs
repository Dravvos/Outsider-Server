using Microsoft.EntityFrameworkCore;

namespace Outsider.PagamentoAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {

        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }


    }
}
