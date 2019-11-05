using AutoMapper;
using GraphQL.Types;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.Admin.Api.GraphQL
{
    public class AdminApiQuery<TUserDtoKey, TRoleDtoKey> : ObjectGraphType
    {
        private readonly IApiResourceService _apiResourceService;
        private readonly IClientService _clientService;
        private readonly IIdentityResourceService _identityResourceService;
        private readonly IPersistedGrantAspNetIdentityService _persistedGrantsService;
        private readonly IMapper _mapper;

        public AdminApiQuery(IApiResourceService apiResourceService, IClientService clientService, IIdentityResourceService identityResourceService, IPersistedGrantAspNetIdentityService persistedGrantsService, IMapper mapper)
        {
            // Store injected dependencies
            _apiResourceService = apiResourceService;
            _clientService = clientService;
            _identityResourceService = identityResourceService;
            _persistedGrantsService = persistedGrantsService;
            _mapper = mapper;

            // Setup the Queries

        }
    }
}
