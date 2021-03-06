﻿namespace GalacticNetwork.STS.Identity.Configuration.Constants
{
    public class ConfigurationConsts
    {
        public const string AdminConnectionStringKey = "AdminConnection";
        public const string ConfigurationDbConnectionStringKey = "ConfigurationDbConnection";
        public const string PersistedGrantDbConnectionStringKey = "PersistedGrantDbConnection";
        public const string IdentityDbConnectionStringKey= "IdentityDbConnection";
        public const string SerilogDbConnectionStringKey = "Serilog:WriteTo:1:Args:connectionString";

        public const string ResourcesPath = "Resources";
        public const string AdminConfigurationKey = "AdminConfiguration";
        public const string RegisterConfiguration = "RegisterConfiguration";
    }
}