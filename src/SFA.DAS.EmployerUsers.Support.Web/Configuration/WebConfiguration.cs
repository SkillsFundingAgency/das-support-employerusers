
using System;
using Newtonsoft.Json;
using StructureMap;

namespace SFA.DAS.EmployerUsers.Support.Web.Configuration
{
    public class WebConfiguration : IWebConfiguration
    {
        [JsonRequired]
        public EmployerUsersApiConfiguration EmployerUsersApi { get; set; }

        [JsonRequired] 
        public SiteConnectorSettings SiteConnector { get; set; }

    }
}