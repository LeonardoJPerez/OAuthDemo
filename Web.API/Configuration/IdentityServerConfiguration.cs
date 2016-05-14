using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Web.API.configurationConfiguration
{
    public static class IdentityServerConfiguration
    {
        /// <summary>
        /// Uses the IdentityServer3 OAuth authentication server.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        public static void UseIdentityServer(this IApplicationBuilder app, IHostingEnvironment env, IConfigurationRoot configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseJwtBearerAuthentication(options =>
            {
                if (env.IsDevelopment())
                {
                    // For development it is not required https in Authority Uri
                    options.RequireHttpsMetadata = false;
                }
                options.Authority = configuration["AuthorizationServer:Authority"];
                options.Audience = configuration["AuthorizationServer:Audience"]; //should this be URL for the receiver of the token
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
            });
        }
    }
}