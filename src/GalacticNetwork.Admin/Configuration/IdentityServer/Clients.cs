using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using GalacticNetwork.Admin.Configuration.Interfaces;

namespace GalacticNetwork.Admin.Configuration.IdentityServer

{
    public class Clients
    {
        public static IEnumerable<Client> GetAdminClient(IAdminConfiguration adminConfiguration)
        {

            return new List<Client>
            {

	            ///////////////////////////////////////////
	            // GalacticNetwork.Admin Client
	            //////////////////////////////////////////
	            new Client
                {

                    ClientId = adminConfiguration.ClientId,
                    ClientName =adminConfiguration.ClientId,
                    ClientUri = adminConfiguration.IdentityAdminBaseUrl,

                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret(adminConfiguration.ClientSecret.ToSha256())
                    },

                    RedirectUris = {
                        $"{adminConfiguration.IdentityAdminBaseUrl}/signin-oidc",
                        $"http://localhost:9000/signin-oidc"
                    },
                    FrontChannelLogoutUri = $"{adminConfiguration.IdentityAdminBaseUrl}/signout-oidc",
                    PostLogoutRedirectUris = {
                        $"{adminConfiguration.IdentityAdminBaseUrl}/signout-callback-oidc",
                        $"http://localhost:9000/signout-callback-oidc"
                    },
                    AllowedCorsOrigins = {
                        adminConfiguration.IdentityAdminBaseUrl,
                        $"http://localhost:9000"
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    }
                },

                ///////////////////////////////////////////
	            // GalacticNetwork.Admin.API Client
	            //////////////////////////////////////////
                new Client
                {
                    ClientId = adminConfiguration.IdentityAdminApiSwaggerUIClientId,
                    ClientName = adminConfiguration.IdentityAdminApiSwaggerUIClientId,

                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = new List<string>
                    {
                        adminConfiguration.IdentityAdminApiSwaggerUIRedirectUrl,
                        "http://localhost:5001/swagger/oauth2-redirect.html"
                    },
                    AllowedScopes =
                    {
                        adminConfiguration.IdentityAdminApiScope
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };

        }
    }
}
