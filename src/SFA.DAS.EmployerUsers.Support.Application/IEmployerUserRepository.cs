using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DAS.Support.Shared;
using Sfa.Das.Console.Core.Domain.Model;

namespace Sfa.Das.Console.ApplicationServices
{
    public interface IEmployerUserRepository
    {
        Task<EmployerUser> Get(string id);
        IEnumerable<SearchItem> FindAll();
    }
}
