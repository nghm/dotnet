namespace Books.Identity
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using IdentityServer4.Models;
    using IdentityServer4.Test;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(new[]
                {
                    new ApiResource("postman_api", "Hypermedia.WebApi"),
                    new ApiResource("api", "Hypermedia.WebApi 2"),
                })
                .AddInMemoryClients(new[]
                {
                    new Client
                    {
                        ClientId = "postman-api",
                        ClientName = "Postman Test Client",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                        AccessTokenType = AccessTokenType.Jwt,
                        AllowAccessTokensViaBrowser = true,
                        EnableLocalLogin = true,
                        RedirectUris = { "https://getpostman.com/oauth2/callback" },
                        AllowedScopes =
                        {
                            "postman_api", "api"
                        }
                    },
                    new Client
                    {
                        ClientId = "api",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "api" }
                    }
                })
                .AddTestUsers(new List<TestUser>
                {
                    new TestUser
                    {
                        Password = "admin",
                        Username = "admin",
                        Claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Role, "Admin")
                        }
                    }
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
