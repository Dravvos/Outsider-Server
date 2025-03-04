using Duende.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Outsider.IdentityServer.Configuration;
using Outsider.IdentityServer.Model;
using Outsider.IdentityServer.Model.Context;
using System.Security.Claims;

namespace Outsider.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly PostgreContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(PostgreContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {

            if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

            _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Cliente)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                Nome="Daniel",
                Email="daniddias53@gmail.com",
                EmailConfirmed= true,
                PhoneNumber="+55 (11) 99007-1115",
                Sobrenome="Admin",
                UserName= "Admin-DD",
            };
            
            _userManager.CreateAsync(admin, "*Igowallah2024*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin,IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name,$"{admin.Nome} {admin.Sobrenome}"),
                new Claim(JwtClaimTypes.GivenName,$"{admin.Nome}"),
                new Claim(JwtClaimTypes.FamilyName,$"{admin.Sobrenome}"),
                new Claim(JwtClaimTypes.Role,IdentityConfiguration.Admin)
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                Nome = "Daniel",
                Email = "daniddias53@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11) 99007-1115",
                Sobrenome = "Cliente",
                UserName = "Client-DD"
            };

            _userManager.CreateAsync(client, "*Igowallah2024*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(client, IdentityConfiguration.Cliente).GetAwaiter().GetResult();

            var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name,$"{client.Nome} {client.Sobrenome}"),
                new Claim(JwtClaimTypes.GivenName,$"{client.Nome}"),
                new Claim(JwtClaimTypes.FamilyName,$"{client.Sobrenome}"),
                new Claim(JwtClaimTypes.Role,IdentityConfiguration.Cliente)
            }).Result;
        }
    }
}
