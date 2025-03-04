﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Outsider.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Cliente = "Cliente";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("Outsider","Outsider Server"),
                new ApiScope(name: "read","Read data."),
                new ApiScope(name: "write","Write data."),
                new ApiScope(name: "delete","Delete data."),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                /*
                new Client
                {
                    ClientId="client",
                    ClientSecrets={new Secret("d89b74fb41bb45f590a2fdcaa66339a72642a97c920442c3a69f231055ad2bac3b6e6e6602e3411cb976fa9bae072920n00745da677274b519c434b5042575547".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read","write"}
                },*/
               new Client
                {
                    ClientId="vue-client",
                    ClientSecrets={new Secret("d89b74fb41bb45f590a2fdcaa66339a72642a97c920442c3a69f231055ad2bac3b6e".Sha256())
                    },
                    ClientName = "Outsider",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris={"http://localhost:5173/callback" },
                    PostLogoutRedirectUris={"http://localhost:5173/"},
                    AllowedCorsOrigins={ "http://localhost:5173" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Outsider",
                    },
                    AllowAccessTokensViaBrowser=true,
                    RequireClientSecret=false,
                    RequirePkce=true,
                    AllowOfflineAccess=true,
                }
            };
    }
}