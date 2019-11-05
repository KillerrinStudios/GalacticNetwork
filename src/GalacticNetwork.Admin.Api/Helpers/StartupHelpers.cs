﻿using System;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GalacticNetwork.Admin.Api.Configuration;
using GalacticNetwork.Admin.Api.Configuration.ApplicationParts;
using GalacticNetwork.Admin.Api.Configuration.Constants;
using GalacticNetwork.Admin.Api.Helpers.Localization;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;
using GraphQL;
using GalacticNetwork.Admin.Api.GraphQL;
using GraphQL.Types;

namespace GalacticNetwork.Admin.Api.Helpers
{
    public static class StartupHelpers
    {

        /// <summary>
        /// Register services for MVC
        /// </summary>
        /// <param name="services"></param>
        public static void AddMvcServices<TUserDto, TUserDtoKey, TRoleDto, TRoleDtoKey, TUserKey, TRoleKey,
            TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
            TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
            TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto>(
            this IServiceCollection services)
            where TUserDto : UserDto<TUserDtoKey>, new()
            where TRoleDto : RoleDto<TRoleDtoKey>, new()
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
            where TUserClaim : IdentityUserClaim<TKey>
            where TUserRole : IdentityUserRole<TKey>
            where TUserLogin : IdentityUserLogin<TKey>
            where TRoleClaim : IdentityRoleClaim<TKey>
            where TUserToken : IdentityUserToken<TKey>
            where TRoleDtoKey : IEquatable<TRoleDtoKey>
            where TUserDtoKey : IEquatable<TUserDtoKey>
            where TUsersDto : UsersDto<TUserDto, TUserDtoKey>
            where TRolesDto : RolesDto<TRoleDto, TRoleDtoKey>
            where TUserRolesDto : UserRolesDto<TRoleDto, TUserDtoKey, TRoleDtoKey>
            where TUserClaimsDto : UserClaimsDto<TUserDtoKey>
            where TUserProviderDto : UserProviderDto<TUserDtoKey>
            where TUserProvidersDto : UserProvidersDto<TUserDtoKey>
            where TUserChangePasswordDto : UserChangePasswordDto<TUserDtoKey>
            where TRoleClaimsDto : RoleClaimsDto<TRoleDtoKey>
        {
            services.TryAddTransient(typeof(IGenericControllerLocalizer<>), typeof(GenericControllerLocalizer<>));

            services.AddMvc(o =>
                {
                    o.Conventions.Add(new GenericControllerRouteConvention());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddDataAnnotationsLocalization()
                .ConfigureApplicationPartManager(m =>
                {
                    m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider<TUserDto, TUserDtoKey, TRoleDto, TRoleDtoKey, TUserKey, TRoleKey, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
                        TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
                        TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto>());
                });
        }


        /// <summary>
        /// Register DbContexts for IdentityServer ConfigurationStore and PersistedGrants, Identity and Logging
        /// Configure the connection strings in AppSettings.json
        /// </summary>
        /// <typeparam name="TConfigurationDbContext"></typeparam>
        /// <typeparam name="TPersistedGrantDbContext"></typeparam>
        /// <typeparam name="TLogDbContext"></typeparam>
        /// <typeparam name="TIdentityDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDbContexts<TIdentityDbContext, TConfigurationDbContext, TPersistedGrantDbContext, TLogDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TIdentityDbContext : DbContext
        where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
        where TConfigurationDbContext : DbContext, IAdminConfigurationDbContext
        where TLogDbContext : DbContext, IAdminLogDbContext
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Config DB for identity
            services.AddDbContext<TIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ConfigurationConsts.IdentityDbConnectionStringKey),
                    sql => sql.MigrationsAssembly(migrationsAssembly)));

            // Config DB from existing connection
            services.AddConfigurationDbContext<TConfigurationDbContext>(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(configuration.GetConnectionString(ConfigurationConsts.ConfigurationDbConnectionStringKey),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            });

            // Operational DB from existing connection
            services.AddOperationalDbContext<TPersistedGrantDbContext>(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(configuration.GetConnectionString(ConfigurationConsts.PersistedGrantDbConnectionStringKey),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            });

            // Log DB from existing connection
            services.AddDbContext<TLogDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(ConfigurationConsts.AdminLogDbConnectionStringKey),
                    optionsSql => optionsSql.MigrationsAssembly(migrationsAssembly)));
        }

        /// <summary>
        /// Add authentication middleware for an API
        /// </summary>
        /// <typeparam name="TIdentityDbContext">DbContext for an access to Identity</typeparam>
        /// <typeparam name="TUser">Entity with User</typeparam>
        /// <typeparam name="TRole">Entity with Role</typeparam>
        /// <param name="services"></param>
        /// <param name="adminApiConfiguration"></param>
        public static void AddApiAuthentication<TIdentityDbContext, TUser, TRole>(this IServiceCollection services,
            AdminApiConfiguration adminApiConfiguration) 
            where TIdentityDbContext : DbContext 
            where TRole : class 
            where TUser : class
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = adminApiConfiguration.IdentityServerBaseUrl;
                    options.ApiName = adminApiConfiguration.OidcApiName;

                    // NOTE: This is only for development set for false
                    // For production use - set RequireHttpsMetadata to true!
                    options.RequireHttpsMetadata = false;
                });

            services.AddIdentity<TUser, TRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConsts.AdministrationPolicy,
                    policy => policy.RequireRole(AuthorizationConsts.AdministrationRole));
            });
        }

        public static void AddGraphQL(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<AdminApiQuery>();
            services.AddSingleton<AdminApiMutation>();

            // Build the GraphQL Schema
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new AdminApiSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }
    }
}
