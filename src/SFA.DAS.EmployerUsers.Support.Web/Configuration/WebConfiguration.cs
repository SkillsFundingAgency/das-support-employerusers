
using Newtonsoft.Json;

namespace SFA.DAS.EmployerUsers.Support.Web.Configuration
{
    public class WebConfiguration : IWebConfiguration
    {
        [JsonRequired]
        public EmployerUsersApiConfiguration EmployerUsersApi { get; set; }
    }
}