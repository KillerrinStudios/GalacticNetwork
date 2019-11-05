using GalacticNetwork.Admin.Api.Dtos.ApiResources;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.Clients
{
    public class ApiResourcePropertyType : ObjectGraphType<ApiResourcePropertyApiDto>
    {
        public ApiResourcePropertyType()
        {
            Name = "ApiResourceProperty";

            Field<IntGraphType>("Id", resolve: context => context.Source.Id);
            Field(x => x.Key);
            Field(x => x.Value);
        }
    }
}
