using System.Threading.Tasks;
using ESFA.DAS.Support.Shared;
using Sfa.Das.Console.ApplicationServices.Models;
using Sfa.Das.Console.Core.Domain.Model;

namespace Sfa.Das.Console.ApplicationServices
{
    public interface IEmployerUserRepository
    {
        Task<EmployerUserSearchResults> Search(string searchTerm, int page);

        Task<EmployerUser> Get(string id);
        Task<ResultPage<SearchItem>> FindPage(int limit, int start);
    }
}
