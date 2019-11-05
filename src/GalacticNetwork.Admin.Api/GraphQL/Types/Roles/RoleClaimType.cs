using GalacticNetwork.Admin.Api.Dtos.Roles;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.Roles
{
    public class RoleClaimType<TRoleDtoKey> : ObjectGraphType<RoleClaimApiDto<TRoleDtoKey>>
    {
        public RoleClaimType()
        {
            Name = "RoleClaim";

            Field<IntGraphType>("ClaimId", resolve: context => context.Source.ClaimId);
            Field(x => x.RoleId);
            Field(x => x.ClaimType);
            Field(x => x.ClaimValue);
        }
    }
}
