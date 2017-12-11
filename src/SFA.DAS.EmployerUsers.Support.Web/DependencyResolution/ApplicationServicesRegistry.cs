using System.Diagnostics.CodeAnalysis;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using StructureMap.Configuration.DSL;

namespace SFA.DAS.EmployerUsers.Support.Web.DependencyResolution
{
    [ExcludeFromCodeCoverage]
    public sealed class ApplicationServicesRegistry : Registry
    {
        public ApplicationServicesRegistry()
        {
            For<IMapUserSearchItems>().Use<UserSearchMapper>();
        }
    }
}
