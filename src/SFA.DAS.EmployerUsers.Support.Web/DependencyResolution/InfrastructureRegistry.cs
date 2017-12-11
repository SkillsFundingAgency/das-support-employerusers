using System.Diagnostics.CodeAnalysis;
using SFA.DAS.EmployerUsers.Api.Client;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;
using SFA.DAS.EmployerUsers.Support.Core.Configuration;
using SFA.DAS.EmployerUsers.Support.Core.Services;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using SFA.DAS.EmployerUsers.Support.Infrastructure.DependencyResolution;
using SFA.DAS.EmployerUsers.Support.Infrastructure.Settings;
using SFA.DAS.NLog.Logger;
using StructureMap.Configuration.DSL;

namespace SFA.DAS.EmployerUsers.Support.Web.DependencyResolution
{
    [ExcludeFromCodeCoverage]
    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            For<ILoggingPropertyFactory>().Use<LoggingPropertyFactory>();

            For<IProvideSettings>().Use(c => new AppConfigSettingsProvider(new MachineSettings()));
            For<ILog>().Use(x => new NLogLogger(
                   x.ParentType,
                   x.GetInstance<IRequestContext>(),
                   x.GetInstance<ILoggingPropertyFactory>().GetProperties())).AlwaysUnique();

            For<IConfigurationSettings>().Use<ApplicationSettings>();

            For<IEmployerUsersApiConfiguration>().Use<EmployerUsersApiSettings>();

            For<IEmployerUsersApiClient>().Use("", (ctx) =>
            {
                var empUserApiSettings = ctx.GetInstance<IEmployerUsersApiConfiguration>();
                return new EmployerUsersApiClient(empUserApiSettings);
            });

            For<IEmployerUserRepository>().Use<EmployerUserRepository>();
            For<IEmployerUserHandler>().Use<EmployerUserHandler>();
            
        }
    }
}
