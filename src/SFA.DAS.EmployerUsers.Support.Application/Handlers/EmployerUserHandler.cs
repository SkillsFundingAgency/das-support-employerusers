using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;
using System;
using SFA.DAS.Support.Shared.SearchIndexModel;

namespace SFA.DAS.EmployerUsers.Support.Application.Handlers
{
    public class EmployerUserHandler : IEmployerUserHandler
    {
        private readonly IEmployerUserRepository _userRepository;

        public EmployerUserHandler(IEmployerUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserSearchModel>> FindSearchItems()
        {

            var models = await _userRepository.FindAllDetails();
            return models.Select(m => Map(m)).ToList();
        }

        public UserSearchModel Map(EmployerUser employerUser)
        {
            return new UserSearchModel
            {
                Id = employerUser.Id,
                Email =  string.IsNullOrEmpty(employerUser.Email) ? "NA" : employerUser.Email,
                FirstName = employerUser.FirstName,
                LastName = employerUser.LastName,
                Status = Enum.GetName(typeof(UserStatus), employerUser.Status),
                SearchType = SearchCategory.User
            };
        }
      
    }
}