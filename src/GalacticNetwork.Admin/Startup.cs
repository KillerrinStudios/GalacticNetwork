﻿using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using GalacticNetwork.Admin.Configuration.Interfaces;
using GalacticNetwork.Admin.EntityFramework.Shared.DbContexts;
using GalacticNetwork.Admin.EntityFramework.Shared.Entities.Identity;
using GalacticNetwork.Admin.Helpers;
using System.Diagnostics;
using System;

namespace GalacticNetwork.Admin
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
            HostingEnvironment = env;

            // Apply Secret Configuration
            bool validateConfiguration(string value)
            {
                if (string.IsNullOrWhiteSpace(value)) { return false; }
                else if (value == "<Not-Configured />") { return false; }
                return true;
            }

            IAdminConfiguration adminConfigSection = Configuration.GetSection(Admin.Configuration.Constants.ConfigurationConsts.AdminConfigurationKey) as IAdminConfiguration;
            string adminConfigClientSecret = "";
            if (adminConfigSection != null)
            {
                adminConfigClientSecret = adminConfigSection.ClientSecret;
                if (!validateConfiguration(adminConfigClientSecret))
                {
                    throw new ArgumentException($"AdminConfiguration:ClientSecret is invalid: {adminConfigClientSecret}");
                }
            }
            else
            {
                try { adminConfigClientSecret = Configuration["AdminConfiguration:ClientSecret"]; } catch (Exception) { }
                if (!validateConfiguration(adminConfigClientSecret))
                {
                    try { adminConfigClientSecret = Configuration["AdminConfiguration__ClientSecret"]; } catch (Exception) { }
                    if (!validateConfiguration(adminConfigClientSecret))
                    {
                        throw new ArgumentException($"AdminConfiguration:ClientSecret is invalid: {adminConfigClientSecret}");
                    }
                }
            }

            Admin.Configuration.Constants.AuthenticationConsts.OidcClientSecret = adminConfigClientSecret;
        }

        public IConfigurationRoot Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Get Configuration
            services.ConfigureRootConfiguration(Configuration);
            var rootConfiguration = services.BuildServiceProvider().GetService<IRootConfiguration>();

            // Add DbContexts for Asp.Net Core Identity, Logging and IdentityServer - Configuration store and Operational store
            services.AddDbContexts<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext>(HostingEnvironment, Configuration);

            // Add Asp.Net Core Identity Configuration and OpenIdConnect auth as well
            services.AddAuthenticationServices<AdminIdentityDbContext, UserIdentity, UserIdentityRole>(HostingEnvironment, rootConfiguration.AdminConfiguration);

            // Add exception filters in MVC
            services.AddMvcExceptionFilters();

            // Add all dependencies for IdentityServer Admin
            services.AddAdminServices<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext>();

            // Add all dependencies for Asp.Net Core Identity
            // If you want to change primary keys or use another db model for Asp.Net Core Identity:
            services.AddAdminAspNetIdentityServices<AdminIdentityDbContext, IdentityServerPersistedGrantDbContext, UserDto<string>, string, RoleDto<string>, string, string, string,
                                UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole,
                                UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
                                UsersDto<UserDto<string>, string>, RolesDto<RoleDto<string>, string>, UserRolesDto<RoleDto<string>, string, string>,
                                UserClaimsDto<string>, UserProviderDto<string>, UserProvidersDto<string>, UserChangePasswordDto<string>,
                                RoleClaimsDto<string>, UserClaimDto<string>, RoleClaimDto<string>>();

            // Add all dependencies for Asp.Net Core Identity in MVC - these dependencies are injected into generic Controllers
            // Including settings for MVC and Localization
            // If you want to change primary keys or use another db model for Asp.Net Core Identity:
            services.AddMvcWithLocalization<UserDto<string>, string, RoleDto<string>, string, string, string,
                UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole,
                UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
                UsersDto<UserDto<string>, string>, RolesDto<RoleDto<string>, string>, UserRolesDto<RoleDto<string>, string, string>,
                UserClaimsDto<string>, UserProviderDto<string>, UserProvidersDto<string>, UserChangePasswordDto<string>,
                RoleClaimsDto<string>>();

            // Add authorization policies for MVC
            services.AddAuthorizationPolicies();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.AddLogging(loggerFactory, Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Add custom security headers
            app.UseSecurityHeaders();

            app.UseStaticFiles();

            // Use authentication and for integration tests use custom middleware which is used only in Staging environment
            app.ConfigureAuthenticationServices(env);

            // Use Localization
            app.ConfigureLocalization();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}