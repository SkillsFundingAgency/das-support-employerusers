using System.Collections.Generic;
using System.Linq;
using SFA.DAS.Support.Shared;
using Newtonsoft.Json;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;
using System;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public class UserSearchMapper : IMapUserSearchItems
    {
        public SearchItem Map(EmployerUser user)
        {
            var keywords = new List<string>
            {
                user.Email,
                user.FirstName,
                user.LastName
            };

            var searchUser = new SearchUserModel
            {
                Id = user.Id,
                Email = string.IsNullOrEmpty(user.Email) ? "NA" :user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Status = Enum.GetName(typeof(UserStatus), user.Status)
            };

            return new SearchItem
            {
                SearchId = user.Id,
                Keywords = keywords.Where(x => x != null).ToArray(),
                SearchResultJson = JsonConvert.SerializeObject(searchUser),
                SearchResultCategory = "USER"
            };

           //// Html = $"<div><a href=\"/resource/?key=user&id={arg.Id}\">{arg.FirstName} {arg.LastName}</a></div>",
        }
    }
}