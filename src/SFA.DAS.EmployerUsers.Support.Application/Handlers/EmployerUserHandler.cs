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

        public EmployerUserHandler(IEmployerUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<SearchItem>> FindSearchItems()
        {

            var models = await _userRepository.FindAllDetails();
            return models.Select(MapToSearch).ToList();
        }
        private SearchItem MapToSearch(UserSummaryViewModel user)
        {
            var keywords = new List<string>
            {
                user.FirstName,
                user.LastName,
                user.Email
            };

            return new SearchItem
            {
                SearchId = user.Id,
                Keywords = keywords.Where(x => x != null).ToArray(),
                SearchResultJson = JsonConvert.SerializeObject(user),
                SearchResultCategory = GlobalConstants.SearchResultCategory
            };

            // Html = $"<div><a href=\"/resource/?key=account&id={arg.Id}\">{arg.FirstName} {arg.LastName}</a></div>",
        }
    }
}