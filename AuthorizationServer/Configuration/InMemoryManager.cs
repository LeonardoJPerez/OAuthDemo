using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthorizationServer
{
    public class InMemoryManager
    {
        public List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>()
            {
                new InMemoryUser
                {
                    Subject = "find.leonardo@gmail.com",
                    Username ="leonardop",
                    Password = "1234567",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.Name, "Leonardo Perez")
                    }
                }
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientName = "API Client v1",
                    ClientId = "apiv1",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Roles,
                        "read"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:58997"
                    },
                    Enabled = true
                }
            };
        }

        public IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                StandardScopes.Roles,
                new Scope
                {
                    Name = "read",
                    DisplayName = "Read Data",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("name")
                    }
                },
                new Scope
                {
                    Name = "private",
                    DisplayName = "Private Data",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("name")
                    }
                }
            };
        }
    }
}