using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.EmployerUsers.Api.Client;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    public sealed class EmployerUserRepository : IEmployerUserRepository
    {
        private readonly IEmployerUsersApiClient _client;
        private readonly ILog _logger;
        private int _usersPerPage = 1000;
        public EmployerUserRepository(ILog logger, IEmployerUsersApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IEnumerable<EmployerUser>> FindAllDetails()
        {
            var results = new List<UserSummaryViewModel>();

            var users = await _client.GetPageOfEmployerUsers(1, _usersPerPage);

            if (users!= null)
            {
                _logger.Info($"Total User Pages : {users.TotalPages} ");

                results.AddRange(users.Data);

                for (var i = 2; i <= users.TotalPages; i++)
                {

                    try
                    {
                        var page = await _client.GetPageOfEmployerUsers(i, _usersPerPage);
                        results.AddRange(page.Data);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex,$"Error while loading page:{i}");

                        throw;
                    }


                }
            }

            if(results.Any(x=> x == null))
            {
                throw new Exception("Invalid record state");
            }

            return results.Select(x => MapToEmployerUser(x));
        }

        public async Task<EmployerUser> Get(string id)
        {
            _logger.Debug($"{nameof(IEmployerUsersApiClient)}.{nameof(_client.GetResource)}<{nameof(UserViewModel)}>(\"/api/users/{id}\");");
            var response = await _client.GetResource<UserViewModel>($"/api/users/{id}");
            if (response != null)
                return MapToEmployerUser(response);
            else
            {
                return null as EmployerUser;
            }
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
                FailedLoginAttempts = data.FailedLoginAttempts,
                IsLocked = data.IsLocked
            };
        }

        private EmployerUser MapToEmployerUser(UserSummaryViewModel data)
        {
            return new EmployerUser
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                IsActive = data.IsActive,
                IsLocked = data.IsLocked
            };
        }
    }
}