using System.Configuration;
using SFA.DAS.EmployerUsers.Support.Core.Configuration;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure.Settings
{
    public sealed class ApplicationSettings : IConfigurationSettings
    {
        public string EnvironmentName => ConfigurationManager.AppSettings["EnvironmentName"];

        public string ApplicationName => ConfigurationManager.AppSettings["ApplicationName"];
    }
}
