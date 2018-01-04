using SFA.DAS.EmployerUsers.Api.Client;

namespace SFA.DAS.EmployerUsers.Support.Web.Configuration
{
    public interface IWebConfiguration
    {
        EmployerUsersApiConfiguration EmployerUsersApi { get; set; }
    }
}