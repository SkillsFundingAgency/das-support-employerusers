using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EAS.Account.Api.Types;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public interface IEmployerUserRepository
    {
        Task<IEnumerable<EmployerUser>> FindAllDetails();
        Task<EmployerUser> Get(string id);
        Task<ICollection<AccountDetailViewModel>> GetAccounts(string id);
    }
}