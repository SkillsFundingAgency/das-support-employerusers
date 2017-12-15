using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.Support.Shared;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using Newtonsoft.Json;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;

namespace SFA.DAS.EmployerUsers.Support.Application.Handlers
{
    public class EmployerUserHandler : IEmployerUserHandler
    {
        private readonly IEmployerUserRepository _userRepository;
        private readonly IMapUserSearchItems _mapUserSearchItems;

        public EmployerUserHandler(IEmployerUserRepository userRepository, IMapUserSearchItems mapUserSearchItems)
        {
            _userRepository = userRepository;
            _mapUserSearchItems = mapUserSearchItems;
        }

        public async Task<IEnumerable<SearchItem>> FindSearchItems()
        {

            var models = await _userRepository.FindAllDetails();
            return models.Select(m => _mapUserSearchItems.Map(m)).ToList();
        }
      
    }
}