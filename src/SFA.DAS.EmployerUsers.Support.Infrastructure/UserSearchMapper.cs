using System.Collections.Generic;
using System.Linq;
using ESFA.DAS.Support.Shared;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public class UserSearchMapper : IMapUserSearchItems
    {
        public SearchItem Map(User arg)
        {
            var keywords = new List<string>
            {
                arg.Email,
                arg.FirstName,
                arg.LastName
            };

            return new SearchItem
            {
                SearchId = $"USER-{arg.Id}",
                Html = $"<div><a href=\"/resource/?key=user&id={arg.Id}\">{arg.FirstName} {arg.LastName}</a></div>",
                Keywords = keywords.Where(x => x != null).ToArray()
            };
        }
    }
}