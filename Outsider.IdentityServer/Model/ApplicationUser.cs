using Microsoft.AspNetCore.Identity;

namespace Outsider.IdentityServer.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }


    }
}
