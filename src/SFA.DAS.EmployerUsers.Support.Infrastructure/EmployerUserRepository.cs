using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DAS.Support.Shared;
using SFA.DAS.EmployerUsers.Api.Client;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Core.Configuration;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public sealed class EmployerUserRepository : IEmployerUserRepository
    {
        private readonly IEmployerUsersApiClient _client;
        private readonly IMapUserSearchItems _mapper;
        private readonly ILog _logger;
        private int _accountsPerPage = 1000;
        public EmployerUserRepository(
                ILog logger, 
                IEmployerUsersApiClient client, 
                IMapUserSearchItems mapper)
        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
            
        }

        public async Task<IEnumerable<UserSummaryViewModel>> FindAllDetails()
        {
            var results = new List<UserSummaryViewModel>();

            var users = await _client.GetPageOfEmployerUsers(1, _accountsPerPage);

            results.AddRange(users.Data);

            for (var i = 2; i <= users.TotalPages; i++)
            {
                var page = await _client.GetPageOfEmployerUsers(i, _accountsPerPage);
                results.AddRange(page.Data);
            }
            return results;
        }

      

        public async Task<EmployerUser> Get(string id)
        {
            _logger.Debug($"{nameof(IEmployerUsersApiClient)}.{nameof(_client.GetResource)}<{nameof(UserViewModel)}>(\"/api/users/{id}\");");
            var response = await _client.GetResource<UserViewModel>($"/api/users/{id}");

            return MapToEmployerUser(response);
        }

        private EmployerUser MapToEmployerUser(UserViewModel data)
        {
            return new EmployerUser
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                IsActive = data.IsActive,
                FailedLoginAttempts  = data.FailedLoginAttempts,
                IsLocked = data.IsLocked
            };

        }
    }
}
