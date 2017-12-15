using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;
using SFA.DAS.Support.Shared;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public interface IMapUserSearchItems
    {
        SearchItem Map(EmployerUser user);
    }
}