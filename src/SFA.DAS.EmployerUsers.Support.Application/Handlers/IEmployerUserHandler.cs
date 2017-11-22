using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DAS.Support.Shared;

namespace SFA.DAS.EmployerUsers.Support.Application.Handlers
{
    public interface IEmployerUserHandler
    {
        Task<IEnumerable<SearchItem>> FindSearchItems();
        
    }
}