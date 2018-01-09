using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure;
using SFA.DAS.Configuration;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.EmployerUsers.Api.Client;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using SFA.DAS.EmployerUsers.Support.Infrastructure.DependencyResolution;
using SFA.DAS.EmployerUsers.Support.Infrastructure.Settings;
using SFA.DAS.EmployerUsers.Support.Web.Configuration;
using SFA.DAS.NLog.Logger;
using StructureMap.Configuration.DSL;

namespace SFA.DAS.EmployerUsers.Support.Web.DependencyResolution
{
    [ExcludeFromCodeCoverage]
    public class InfrastructureRegistry : Registry
    {
        private const string ServiceName = "SFA.DAS.Support.EmployerUsers";
        private const string Version = "1.0";
      
        public InfrastructureRegistry()
        {
            
            For<ILoggingPropertyFactory>().Use<LoggingPropertyFactory>();

            For<ILog>().Use(x => new NLogLogger(
                   x.ParentType,
                   x.GetInstance<IRequestContext>(),
                   x.GetInstance<ILoggingPropertyFactory>().GetProperties())).AlwaysUnique();


            WebConfiguration configuration = GetConfiguration();

            For<IWebConfiguration>().Use(configuration);
            For<IEmployerUsersApiConfiguration>().Use( configuration.EmployerUsersApi);




            For<IEmployerUsersApiClient>().Use("", (ctx) =>
            {
                var empUserApiSettings = ctx.GetInstance<IEmployerUsersApiConfiguration>();
                return new EmployerUsersApiClient(empUserApiSettings);
            });

            For<IEmployerUserRepository>().Use<EmployerUserRepository>();
            For<IEmployerUserHandler>().Use<EmployerUserHandler>();
            
        }

        
        private WebConfiguration GetConfiguration()
        {
            var environment = CloudConfigurationManager.GetSetting("EnvironmentName") ?? 
                              "LOCAL";
            var storageConnectionString = CloudConfigurationManager.GetSetting("ConfigurationStorageConnectionString") ??
                                          "UseDevelopmentStorage=true;";

            var configurationRepository = new AzureTableStorageConfigurationRepository(storageConnectionString); ;

            var configurationOptions = new ConfigurationOptions(ServiceName, environment, Version);

            var configurationService = new ConfigurationService(configurationRepository, configurationOptions);

            var webConfiguration = configurationService.Get<WebConfiguration>();    

            return webConfiguration;
        }


    }
}
