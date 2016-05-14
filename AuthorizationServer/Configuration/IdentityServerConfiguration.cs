using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;

namespace AuthorizationServer.ConfigurationExtensions
{
    public static class IdentityServerConfiguration
    {
        public static void UseIdentityServer(this IApplicationBuilder app, IApplicationEnvironment env, IConfigurationRoot configuration)
        {
            var certFile = env.ApplicationBasePath + $"{System.IO.Path.DirectorySeparatorChar}test.pfx";
            var manager = new InMemoryManager();
            var options = new IdentityServerOptions
            {
                SiteName = "Demo Identity Server",
                SigningCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certFile, configuration["SigningPassword"]),
                RequireSsl = true,
                Factory = GetIdentityServerFactory(manager)
            };

            app.UseIdentityServer(options);
        }

        private static IdentityServerServiceFactory GetIdentityServerFactory(InMemoryManager manager)
        {
            var factory = new IdentityServerServiceFactory();
            factory.UseInMemoryClients(manager.GetClients());
            factory.UseInMemoryScopes(manager.GetScopes());
            factory.UseInMemoryUsers(manager.GetUsers());

            var cors = new InMemoryCorsPolicyService(manager.GetClients());
            factory.CorsPolicyService = new Registration<IdentityServer3.Core.Services.ICorsPolicyService>(cors);

            return factory;
        }
    }
}