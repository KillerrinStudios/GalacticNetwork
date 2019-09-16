﻿using AutoMapper;
using GalacticNetwork.Admin.Api.Dtos.Clients;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.Configuration;

namespace GalacticNetwork.Admin.Api.Mappers
{
    public class ClientApiMapperProfile : Profile
    {
        public ClientApiMapperProfile()
        {
            // Client
            CreateMap<ClientDto, ClientApiDto>(MemberList.Destination)
                .ForMember(dest => dest.ProtocolType, opt => opt.Condition(srs => srs != null))
                .ForMember(x => x.ClientSecrets, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ClientsDto, ClientsApiDto>(MemberList.Destination)
                .ReverseMap();

            CreateMap<ClientCloneApiDto, ClientCloneDto>(MemberList.Destination)
                .ReverseMap();

            // Client Secrets
            CreateMap<ClientSecretsDto, ClientSecretApiDto>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientSecretId))
                .ReverseMap();

            CreateMap<ClientSecretDto, ClientSecretApiDto>(MemberList.Destination);
            CreateMap<ClientSecretsDto, ClientSecretsApiDto>(MemberList.Destination);

            // Client Properties
            CreateMap<ClientPropertiesDto, ClientPropertyApiDto>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientPropertyId))
                .ReverseMap();

            CreateMap<ClientPropertyDto, ClientPropertyApiDto>(MemberList.Destination);
            CreateMap<ClientPropertiesDto, ClientPropertiesApiDto>(MemberList.Destination);

            // Client Claims
            CreateMap<ClientClaimsDto, ClientClaimApiDto>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientClaimId))
                .ReverseMap();

            CreateMap<ClientClaimDto, ClientClaimApiDto>(MemberList.Destination);
            CreateMap<ClientClaimsDto, ClientClaimsApiDto>(MemberList.Destination);
        }
    }
}