using Sfa.Das.Console.Infrastructure;
using SFA.DAS.EmployerUsers.Support.Web.Services;
using StructureMap;

namespace Sfa.Das.Console.Web.DependencyResolution
{
    public sealed class ApplicationServicesRegistry : Registry
    {
        public ApplicationServicesRegistry()
        {
            For<IMapUserSearchItems>().Use<UserSearchMapper>();
        }
    }
}
