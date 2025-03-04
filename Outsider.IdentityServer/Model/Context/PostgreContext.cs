using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Outsider.IdentityServer.Model.Context
{
    public class PostgreContext : IdentityDbContext<ApplicationUser>
    {
        public PostgreContext()
        {

        }
        public PostgreContext(DbContextOptions<PostgreContext> options) : base(options) { }

    }
}
