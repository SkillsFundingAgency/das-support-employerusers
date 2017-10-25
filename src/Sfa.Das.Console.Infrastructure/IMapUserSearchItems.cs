using ESFA.DAS.Support.Shared;
using SFA.DAS.EmployerUsers.Api.Types;

namespace SFA.DAS.EmployerUsers.Support.Web.Services
{
    public interface IMapUserSearchItems
    {
        SearchItem Map(UserSummaryViewModel arg);
    }
}