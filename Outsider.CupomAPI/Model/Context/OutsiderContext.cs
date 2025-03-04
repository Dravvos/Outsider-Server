using Microsoft.EntityFrameworkCore;

namespace Outsider.CupomAPI.Model.Context
{
    public class OutsiderContext : DbContext
    {

        public OutsiderContext() { }
        public OutsiderContext(DbContextOptions<OutsiderContext> options) : base(options) { }

        public DbSet<CupomModel> Cupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CupomModel>().HasData(new CupomModel
            {
                Id = Guid.NewGuid(),
                DataInclusao = DateTime.Now,
                PorcentagemDesconto = 10,
                CodigoCupom = "DN10"
            });
            modelBuilder.Entity<CupomModel>().HasData(new CupomModel
            {
                Id = Guid.NewGuid(),
                DataInclusao = DateTime.Now,
                PorcentagemDesconto = 20,
                CodigoCupom = "DN20"
            });
        }
    }
}
