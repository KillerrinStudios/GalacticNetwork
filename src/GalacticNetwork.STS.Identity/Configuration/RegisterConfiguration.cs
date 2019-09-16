using GalacticNetwork.STS.Identity.Configuration.Intefaces;

namespace GalacticNetwork.STS.Identity.Configuration
{
    public class RegisterConfiguration : IRegisterConfiguration
    {
        public bool Enabled { get; set; } = true;
    }
}
