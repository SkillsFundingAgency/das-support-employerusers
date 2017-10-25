using MediatR;
using Sfa.Das.Console.ApplicationServices.Services;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Sfa.Das.Console.ApplicationServices.DependencyResolution
{
    public sealed class ApplicationServicesRegistry : Registry
    {
        public ApplicationServicesRegistry()
        {
            this.Scan(
                scan =>
                {
                    scan.AssemblyContainingType<ApplicationServicesRegistry>();
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.AddAllTypesOf(typeof(IRequestHandler<,>));
                    scan.AddAllTypesOf(typeof(IAsyncRequestHandler<,>));
                });
            this.For<IDatetimeService>().Use<DatetimeService>();
        }
    }
}
