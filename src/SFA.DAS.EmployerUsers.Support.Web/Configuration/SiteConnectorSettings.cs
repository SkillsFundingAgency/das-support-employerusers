namespace SFA.DAS.EmployerUsers.Support.Web.Configuration
{
    public class SiteConnectorSettings : ISiteConnectorSettings
    {
        public string Audience { get; set; }
        public string Scope { get; set; }
        public string Tenant { get; set; }
    }
}