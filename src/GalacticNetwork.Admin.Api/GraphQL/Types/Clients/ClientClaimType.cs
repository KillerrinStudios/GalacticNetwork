using GalacticNetwork.Admin.Api.Dtos.Clients;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.Clients
{
    public class ClientClaimType : ObjectGraphType<ClientClaimApiDto>
    {
        public ClientClaimType()
        {
            Name = "ClientClaim";

            Field<IntGraphType>("Id", resolve: context => context.Source.Id);
            Field(x => x.Type);
            Field(x => x.Value);
        }
    }
}
