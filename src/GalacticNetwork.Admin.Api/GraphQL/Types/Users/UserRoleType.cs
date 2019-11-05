using GalacticNetwork.Admin.Api.Dtos.Users;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.Users
{
    public class UserRoleType : ObjectGraphType<UserRoleApiDto<string, string>>
    {
        public UserRoleType()
        {
            Name = "UserRole";

            Field<StringGraphType>("UserId", resolve: context => context.Source.UserId);
            Field<StringGraphType>("RoleId", resolve: context => context.Source.RoleId);
        }
    }
}
