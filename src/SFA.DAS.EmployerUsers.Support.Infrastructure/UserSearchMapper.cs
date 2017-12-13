using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Support.Shared;
using Newtonsoft.Json;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public class UserSearchMapper : IMapUserSearchItems
    {
        public SearchItem Map(User user)
        {
            var keywords = new List<string>
            {
                user.Email,
                user.FirstName,
                user.LastName
            };

            return new SearchItem
            {
                SearchId = user.Id,
                Keywords = keywords.Where(x => x != null).ToArray(),
                SearchResultJson = JsonConvert.SerializeObject(user),
                SearchResultCategory = "USER"
            };

           //// Html = $"<div><a href=\"/resource/?key=user&id={arg.Id}\">{arg.FirstName} {arg.LastName}</a></div>",
        }
    }
}