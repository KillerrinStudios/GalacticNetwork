using GalacticNetwork.Admin.Api.Dtos.PersistedGrants;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.PersistedGrants
{
    public class PersistedGrantType : ObjectGraphType<PersistedGrantApiDto>
    {
        public PersistedGrantType()
        {
            Name = "PersistedGrant";

            Field<StringGraphType>("Key", resolve: context => context.Source.Key.ToString());
            Field(x => x.Type);
            Field(x => x.SubjectId);
            Field(x => x.SubjectName);
            Field(x => x.ClientId);
            Field(x => x.CreationTime);
            Field(x => x.Expiration);
            Field(x => x.Data);
        }
    }
}
