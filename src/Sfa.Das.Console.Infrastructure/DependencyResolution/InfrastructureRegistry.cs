using Sfa.Das.Console.ApplicationServices;
using Sfa.Das.Console.Core.Configuration;
using Sfa.Das.Console.Core.Services;
using Sfa.Das.Console.Infrastructure.Settings;
using SFA.DAS.EmployerUsers.Api.Client;
using SFA.DAS.NLog.Logger;
using StructureMap.Configuration.DSL;

namespace Sfa.Das.Console.Infrastructure.DependencyResolution
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
            For<IEmployerUsersApiConfiguration>().Use<Settings.EmployerUsersApiConfiguration>();

            For<IEmployerUserRepository>().Use<EmployerUserRepository>();
        }
    }
}
