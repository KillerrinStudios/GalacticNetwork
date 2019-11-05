using GalacticNetwork.Admin.Api.Dtos.ApiResources;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.Clients
{
    public class ApiScopeType : ObjectGraphType<ApiScopeApiDto>
    {
        public ApiScopeType()
        {
            Name = "ApiScope";

            Field<IntGraphType>("Id", resolve: context => context.Source.Id);
            Field(x => x.ShowInDiscoveryDocument);
            Field(x => x.Name);
            Field(x => x.DisplayName);
            Field(x => x.Description);
            Field(x => x.Required);
            Field(x => x.Emphasize);
            Field<ListGraphType>("UserClaims", resolve: context => context.Source.UserClaims);
        }
    }
}
