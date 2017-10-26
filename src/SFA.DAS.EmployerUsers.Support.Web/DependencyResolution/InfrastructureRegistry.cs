using Sfa.Das.Console.ApplicationServices;
using Sfa.Das.Console.Core.Configuration;
using Sfa.Das.Console.Core.Services;
using Sfa.Das.Console.Infrastructure;
using Sfa.Das.Console.Infrastructure.DependencyResolution;
using Sfa.Das.Console.Infrastructure.Settings;
using SFA.DAS.EmployerUsers.Api.Client;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using SFA.DAS.NLog.Logger;
using StructureMap.Configuration.DSL;

namespace SFA.DAS.EmployerUsers.Support.Web.DependencyResolution
{
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

            For<IEmployerUsersApiClient>().Use("", (ctx) =>
            {
                var empUserApiSettings = ctx.GetInstance<IEmployerUsersApiConfiguration>();
                return new EmployerUsersApiClient(empUserApiSettings);
            });
            For<IEmployerUsersApiConfiguration>().Use<EmployerUsersApiConfiguration>();

            For<IEmployerUserRepository>().Use<EmployerUserRepository>();

            For<IEmployerUserDatabaseSettings>().Use<EmployerUserDatabaseSettings>();
        }
    }
}
