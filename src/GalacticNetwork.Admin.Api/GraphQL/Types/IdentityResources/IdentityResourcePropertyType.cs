using GalacticNetwork.Admin.Api.Dtos.IdentityResources;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.IdentityResources
{
    public class IdentityResourcePropertyType : ObjectGraphType<IdentityResourcePropertyApiDto>
    {
        public IdentityResourcePropertyType()
        {
            Name = "IdentityResourceProperty";

            Field<IntGraphType>("Id", resolve: context => context.Source.Id);
            Field(x => x.Key);
            Field(x => x.Value);
        }
    }
}
