using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.Support.Shared;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Infrastructure;

namespace SFA.DAS.EmployerUsers.Support.Application.Handlers
{
    public class EmployerUserHandler : IEmployerUserHandler
    {
        private readonly IEmployerUserRepository _userRepository;

        public EmployerUserHandler(IEmployerUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<SearchItem>> FindSearchItems()
        {

            var models = await _userRepository.FindAllDetails();
            return models.Select(MapToSearch).ToList();
        }
        private SearchItem MapToSearch(UserSummaryViewModel arg)
        {
            var keywords = new List<string>
            {
                arg.Id,
                arg.FirstName,
                arg.LastName,
                arg.Email
            };

            
            return new SearchItem
            {
                SearchId = $"ACC-{arg.Id}",
                Html = $"<div><a href=\"/resource/?key=account&id={arg.Id}\">{arg.FirstName} {arg.LastName}</a></div>",
                Keywords = keywords.Where(x => x != null).ToArray()
            };
        }
    }
}