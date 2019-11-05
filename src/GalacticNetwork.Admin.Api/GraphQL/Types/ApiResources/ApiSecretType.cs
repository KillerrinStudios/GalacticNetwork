using GalacticNetwork.Admin.Api.Dtos.ApiResources;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.ApiResources
{
    public class ApiSecretType : ObjectGraphType<ApiSecretApiDto>
    {
        public ApiSecretType()
        {
            Name = "ApiSecret";

            Field<IntGraphType>("Id", resolve: context => context.Source.Id);
            Field(x => x.Type);
            Field(x => x.Description);
            Field(x => x.Value);
            Field(x => x.Expiration);
        }
    }
}
