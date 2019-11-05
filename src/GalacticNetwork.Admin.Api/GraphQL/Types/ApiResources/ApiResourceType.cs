using GalacticNetwork.Admin.Api.Dtos.ApiResources;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.ApiResources
{
    public class ApiResourceType : ObjectGraphType<ApiResourceApiDto>
    {
        public ApiResourceType()
        {
            Name = "ApiResource";

            Field<IntGraphType>("Id", resolve: context => context.Source.Id);
            Field(x => x.Name);
            Field(x => x.DisplayName);
            Field(x => x.Description);
            Field(x => x.Enabled);
            Field<ListGraphType>("UserClaims", resolve: context => context.Source.UserClaims);
        }
    }
}
