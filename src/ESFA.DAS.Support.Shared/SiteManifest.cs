using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ESFA.DAS.Support.Shared
{
    [ExcludeFromCodeCoverage]
    public class SiteManifest: ISiteManifest
    {
        public string Version { get; set;  }
        public IEnumerable<SiteResource> Resources { get; set; }
        public string BaseUrl { get; set; }
        public IEnumerable<SiteChallenge> Challenges { get; set; }
    }
}