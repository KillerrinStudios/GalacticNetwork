using GalacticNetwork.Admin.Api.Dtos.Users;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.Clients
{
    public class UserProviderType : ObjectGraphType<UserProviderApiDto<string>>
    {
        public UserProviderType()
        {
            Name = "UserProvider";

            Field<StringGraphType>("UserId", resolve: context => context.Source.UserId);
            Field(x => x.UserName);
            Field(x => x.ProviderKey);
            Field(x => x.LoginProvider);
            Field(x => x.ProviderDisplayName);
        }
    }
}
