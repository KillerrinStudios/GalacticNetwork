using GalacticNetwork.Admin.Api.Dtos.Users;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.Users
{
    public class UserClaimType<TUserDtoKey> : ObjectGraphType<UserClaimApiDto<TUserDtoKey>>
    {
        public UserClaimType()
        {
            Name = "UserClaim";

            Field<StringGraphType>("UserId", resolve: context => context.Source.UserId);
            Field(x => x.ClaimId);
            Field(x => x.ClaimType);
            Field(x => x.ClaimValue);
        }
    }
}
