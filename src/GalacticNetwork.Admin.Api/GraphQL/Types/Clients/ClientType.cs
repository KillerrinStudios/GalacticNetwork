using GalacticNetwork.Admin.Api.Dtos.Clients;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.Clients
{
    public class ClientType : ObjectGraphType<ClientApiDto>
    {
        public ClientType()
        {
            Name = "Client";

            Field<IntGraphType>("Id", resolve: context => context.Source.Id);
            Field(x => x.AbsoluteRefreshTokenLifetime);
            Field(x => x.AccessTokenLifetime);
            Field(x => x.ConsentLifetime);
            Field(x => x.AccessTokenType);
            Field(x => x.AllowAccessTokensViaBrowser);
            Field(x => x.AllowOfflineAccess);
            Field(x => x.AllowPlainTextPkce);
            Field(x => x.AllowRememberConsent);
            Field(x => x.AlwaysIncludeUserClaimsInIdToken);
            Field(x => x.AlwaysSendClientClaims);
            Field(x => x.AuthorizationCodeLifetime);

            Field(x => x.FrontChannelLogoutUri);
            Field(x => x.FrontChannelLogoutSessionRequired);
            Field(x => x.BackChannelLogoutUri);
            Field(x => x.BackChannelLogoutSessionRequired);

            Field(x => x.ClientId);
            Field(x => x.ClientName);
            Field(x => x.ClientUri);
            Field(x => x.Description);
            Field(x => x.Enabled);
            Field(x => x.EnableLocalLogin);
            Field(x => x.IdentityTokenLifetime);

            Field(x => x.IncludeJwtId);
            Field(x => x.LogoUri);
            Field(x => x.ClientClaimsPrefix);
            Field(x => x.PairWiseSubjectSalt);
            Field(x => x.ProtocolType);
            Field(x => x.RefreshTokenExpiration);
            Field(x => x.RefreshTokenUsage);
            Field(x => x.SlidingRefreshTokenLifetime);
            Field(x => x.RequireClientSecret);
            Field(x => x.RequireConsent);
            Field(x => x.RequirePkce);
            Field(x => x.UpdateAccessTokenClaimsOnRefresh);

            Field<ListGraphType>("PostLogoutRedirectUris", resolve: context => context.Source.PostLogoutRedirectUris);
            Field<ListGraphType>("IdentityProviderRestrictions", resolve: context => context.Source.IdentityProviderRestrictions);
            Field<ListGraphType>("RedirectUris", resolve: context => context.Source.RedirectUris);
            Field<ListGraphType>("AllowedCorsOrigins", resolve: context => context.Source.AllowedCorsOrigins);
            Field<ListGraphType>("AllowedGrantTypes", resolve: context => context.Source.AllowedGrantTypes);
            Field<ListGraphType>("AllowedScopes", resolve: context => context.Source.AllowedScopes);

            Field<ListGraphType<ClientClaimType>>("Claims", resolve: context => context.Source.Claims);
            Field<ListGraphType<ClientSecretType>>("ClientSecrets", resolve: context => context.Source.ClientSecrets);
            Field<ListGraphType<ClientPropertyType>>("Properties", resolve: context => context.Source.Properties);

            Field(x => x.Updated);
            Field(x => x.LastAccessed);
            Field(x => x.UserSsoLifetime);
            Field(x => x.UserCodeType);
            Field(x => x.DeviceCodeLifetime);
        }
    }
}
