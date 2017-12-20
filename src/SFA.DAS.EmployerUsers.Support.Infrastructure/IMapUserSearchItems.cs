using SFA.DAS.Support.Shared;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public interface IMapUserSearchItems
    {
        SearchItem Map(User arg);
    }
}