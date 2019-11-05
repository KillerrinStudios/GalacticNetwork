using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL
{
    public class AdminApiSchema<TUserDtoKey, TRoleDtoKey> : Schema
    {
        public AdminApiSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<AdminApiQuery>();
            Mutation = resolver.Resolve<AdminApiMutation>();
        }
    }
}
