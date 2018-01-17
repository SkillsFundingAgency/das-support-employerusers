namespace SFA.DAS.EmployerUsers.Support.Web.Configuration
{
    public interface ISiteConnectorSettings
    {
        string Audience { get; set; }
        string Scope { get; set; }
        string Tenant { get; set; }
    }

   
}