using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ESFA.DAS.Support.Shared;
using Microsoft.Azure;
using Sfa.Das.Console.ApplicationServices;
using Sfa.Das.Console.Core.Configuration;
using Sfa.Das.Console.Core.Domain.Model;
using SFA.DAS.EmployerUsers.Api.Client;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Console.Infrastructure
{
    public sealed class EmployerUserRepository : IEmployerUserRepository
    {
        private readonly IEmployerUsersApiClient _client;
        private readonly IMapUserSearchItems _mapper;
        private readonly IEmployerUserDatabaseSettings _settings;
        private readonly ILog _logger;

        public EmployerUserRepository(ILog logger, IEmployerUsersApiClient client, IMapUserSearchItems mapper, IEmployerUserDatabaseSettings settings)
        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
            _settings = settings;
        }

        public IEnumerable<SearchItem> FindAll()
        {
            return GetUsers(Int32.MaxValue, 0).Select(_mapper.Map);
        }

        public User[] GetUsers(int limit, int start)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.Query<User>(@"GetUsers @pageSize, @offSet", new { pageSize = limit, offset = start }).ToArray();
            }
        }

        protected SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_settings.ConnectionString);
            try
            {
                connection.Open();
            }
            catch
            {
                connection.Dispose();
                throw;
            }

            return connection;
        }

        public async Task<EmployerUser> Get(string id)
        {
            try
            {
                _logger.Debug($"IEmployerUsersApiClient.GetResource<UserViewModel>(\"/api/users/{id}\");");
                var response = await _client.GetResource<UserViewModel>($"/api/users/{id}");

                return MapToEmployerUser(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failure connecting to DAS Employer Users API");
            }

            return null;
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
    }
}
