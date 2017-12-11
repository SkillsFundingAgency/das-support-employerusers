using System.Diagnostics.CodeAnalysis;

namespace ESFA.DAS.Support.Shared
{
    [ExcludeFromCodeCoverage]
    public class NavItem
    {
        public string Title { get; set; }
        public string Href { get; set; }
        public string Key { get; set; }
    }
}
