using SFA.DAS.EmployerUsers.Support.Infrastructure;
using StructureMap.Configuration.DSL;

namespace SFA.DAS.EmployerUsers.Support.Web.DependencyResolution
{
    public sealed class ApplicationServicesRegistry : Registry
    {
        public ApplicationServicesRegistry()
        {
            For<IMapUserSearchItems>().Use<UserSearchMapper>();
        }
    }
}
