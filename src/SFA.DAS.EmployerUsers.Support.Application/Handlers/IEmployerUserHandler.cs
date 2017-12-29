using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using SFA.DAS.Support.Shared;

namespace SFA.DAS.EmployerUsers.Support.Application.Handlers
{
    public interface IEmployerUserHandler
    {
        Task<IEnumerable<UserSearchModel>> FindSearchItems();
        
    }
}