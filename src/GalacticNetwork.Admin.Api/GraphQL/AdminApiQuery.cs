using AutoMapper;
using GalacticNetwork.Admin.Api.Controllers;
using GalacticNetwork.Admin.Api.Dtos.IdentityResources;
using GalacticNetwork.Admin.Api.Dtos.PersistedGrants;
using GalacticNetwork.Admin.Api.GraphQL.Types.IdentityResources;
using GalacticNetwork.Admin.Api.GraphQL.Types.PersistedGrants;
using GalacticNetwork.Admin.Api.Helpers;
using GalacticNetwork.Admin.Api.Mappers;
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

        public AdminApiQuery(IApiResourceService apiResourceService, IClientService clientService, IIdentityResourceService identityResourceService,
            IPersistedGrantAspNetIdentityService persistedGrantsService, IMapper mapper)
        {
            // Store injected dependencies
            _apiResourceService = apiResourceService;
            _clientService = clientService;
            _identityResourceService = identityResourceService;
            _persistedGrantsService = persistedGrantsService;
            _mapper = mapper;

            // Setup the Queries
            SetupApiResources();
            SetupClients();
            SetupIdentityResources();
            SetupPersistedGrants();
            SetupRoles();
            SetupUsers();
        }

        private void SetupApiResources()
        {

        }

        private void SetupClients()
        {

        }

        private void SetupIdentityResources()
        {
            FieldAsync<IdentityResourceType>("IdentityResource",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: async context =>
                {
                    if (context.HasArgument("id"))
                    {
                        var id = context.GetArgument<int>("id");
                        var identityResourceDto = await _identityResourceService.GetIdentityResourceAsync(id);
                        var identityResourceApiModel = identityResourceDto.ToIdentityResourceApiModel<IdentityResourceApiDto>();
                        return identityResourceApiModel;
                    }

                    return new ArgumentNullException("id");
                }
            );

            FieldAsync<ListGraphType<IdentityResourceType>>("IdentityResources",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "search", DefaultValue = "" },
                    new QueryArgument<IntGraphType> { Name = "page", DefaultValue = 1 },
                    new QueryArgument<IntGraphType> { Name = "pageSize", DefaultValue = 10 }
                ),
                resolve: async context =>
                {
                    var searchText = context.GetArgument<string>("search");
                    var page = context.GetArgument<int>("page");
                    var pageSize = context.GetArgument<int>("pageSize");

                    var identityResourcesDto = await _identityResourceService.GetIdentityResourcesAsync(searchText, page, pageSize);
                    var identityResourcesApiDto = identityResourcesDto.ToIdentityResourceApiModel<IdentityResourcesApiDto>();

                    return identityResourcesApiDto.IdentityResources;
                }
            );

            FieldAsync<IdentityResourceType>("IdentityResourceProperty",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ),
                resolve: async context =>
                {
                    if (context.HasArgument("id"))
                    {
                        var id = context.GetArgument<int>("id");
                        var identityResourcePropertiesDto = await _identityResourceService.GetIdentityResourcePropertyAsync(id);
                        var identityResourcePropertyApiDto = identityResourcePropertiesDto.ToIdentityResourceApiModel<IdentityResourcePropertyApiDto>();
                        return identityResourcePropertyApiDto;
                    }

                    return new ArgumentNullException("id");
                }
            );

            FieldAsync<ListGraphType<IdentityResourceType>>("IdentityResourcesProperties",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" },
                    new QueryArgument<IntGraphType> { Name = "page", DefaultValue = 1 },
                    new QueryArgument<IntGraphType> { Name = "pageSize", DefaultValue = 10 }
                ),
                resolve: async context =>
                {
                    if (context.HasArgument("id"))
                    {
                        var id = context.GetArgument<int>("id");
                        var page = context.GetArgument<int>("page");
                        var pageSize = context.GetArgument<int>("pageSize");

                        var identityResourcePropertiesDto = await _identityResourceService.GetIdentityResourcePropertiesAsync(id, page, pageSize);
                        var identityResourcePropertiesApiDto = identityResourcePropertiesDto.ToIdentityResourceApiModel<IdentityResourcePropertiesApiDto>();

                        return identityResourcePropertiesApiDto.IdentityResourceProperties;
                    }
                    return new ArgumentNullException("id");
                }
            );
        }

        private void SetupPersistedGrants()
        {
            FieldAsync<PersistedGrantType>("PersistedGrant",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id" }
                ),
                resolve: async context =>
                {
                    if (context.HasArgument("id"))
                    {
                        var id = context.GetArgument<string>("id");
                        var persistedGrantDto = await _persistedGrantsService.GetPersistedGrantAsync(UrlHelpers.QueryStringUnSafeHash(id));
                        var persistedGrantApiDto = persistedGrantDto.ToPersistedGrantApiModel<PersistedGrantApiDto>();
                        return persistedGrantApiDto;
                    }

                    return new ArgumentNullException("id");
                }
            );

            FieldAsync<ListGraphType<PersistedGrantType>>("PersistedGrants",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "search", DefaultValue = "" },
                    new QueryArgument<IntGraphType> { Name = "page", DefaultValue = 1 },
                    new QueryArgument<IntGraphType> { Name = "pageSize", DefaultValue = 10 }
                ),
                resolve: async context =>
                {
                    var searchText = context.GetArgument<string>("search");
                    var page = context.GetArgument<int>("page");
                    var pageSize = context.GetArgument<int>("pageSize");

                    var persistedGrantsDto = await _persistedGrantsService.GetPersistedGrantsByUsersAsync(searchText, page, pageSize);
                    var persistedGrantsApiDto = persistedGrantsDto.ToPersistedGrantApiModel<PersistedGrantsApiDto>();

                    return persistedGrantsApiDto.PersistedGrants;
                }
            );
        }

        private void SetupRoles()
        {

        }

        private void SetupUsers()
        {

        }
    }
}
