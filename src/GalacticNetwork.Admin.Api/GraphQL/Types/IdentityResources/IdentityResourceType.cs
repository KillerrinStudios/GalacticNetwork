using GalacticNetwork.Admin.Api.Dtos.IdentityResources;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.IdentityResources
{
    public class IdentityResourceType : ObjectGraphType<IdentityResourceApiDto>
    {
        public IdentityResourceType()
        {
            Name = "IdentityResource";
            
            Field<IntGraphType>("Id", resolve: context => context.Source.Id);
            Field(x => x.Name);
            Field(x => x.DisplayName);
            Field(x => x.Description);
            Field(x => x.Enabled);
            Field(x => x.ShowInDiscoveryDocument);
            Field(x => x.Required);
            Field(x => x.Emphasize);
            //Field<ListGraphType>("UserClaims", resolve: context => context.Source.UserClaims);
        }

    }
}
