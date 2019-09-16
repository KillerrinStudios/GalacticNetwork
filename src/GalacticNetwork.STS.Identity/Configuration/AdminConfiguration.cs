using GalacticNetwork.STS.Identity.Configuration.Intefaces;

namespace GalacticNetwork.STS.Identity.Configuration
{
    public class AdminConfiguration : IAdminConfiguration
    {
        public string IdentityAdminBaseUrl { get; set; } = "http://localhost:9000";
    }
}