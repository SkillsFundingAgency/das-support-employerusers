using System.Diagnostics.CodeAnalysis;

namespace ESFA.DAS.Support.Shared
{
    [ExcludeFromCodeCoverage]
    public class PageLinks
    {
        public string Prev { get; set; }
        public string Self { get; set; }
        public string Next { get; set; }
    }
}