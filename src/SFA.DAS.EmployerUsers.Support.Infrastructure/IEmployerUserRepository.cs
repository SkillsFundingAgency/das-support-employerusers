using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DAS.Support.Shared;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public interface IEmployerUserRepository
    {
        Task<IEnumerable<UserViewModel>> FindAllDetails();
        Task<EmployerUser> Get(string id);
    }
}