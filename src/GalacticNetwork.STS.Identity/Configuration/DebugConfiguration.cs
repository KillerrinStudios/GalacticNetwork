using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticNetwork.STS.Identity.Configuration
{
    public class DebugConfiguration
    {
        public string ConfigurationDbConnection { get; set; }
        public string PersistedGrantDbConnection { get; set; }
        public string IdentityDbConnection { get; set; }
        public string LoggingDbConnection { get; set; }
    }
}
