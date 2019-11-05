using GalacticNetwork.Admin.Api.Dtos.PersistedGrants;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL.Types.IdentityResources
{
    public class PersistedGrantSubjectType : ObjectGraphType<PersistedGrantSubjectApiDto>
    {
        public PersistedGrantSubjectType()
        {
            Name = "PersistedGrantSubject";

            Field<StringGraphType>("SubjectId", resolve: context => context.Source.SubjectId.ToString());
            Field(x => x.SubjectName);
        }
    }
}
