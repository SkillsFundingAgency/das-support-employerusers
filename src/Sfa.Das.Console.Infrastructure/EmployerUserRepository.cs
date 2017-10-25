using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DAS.Support.Shared;
using Sfa.Das.Console.ApplicationServices;
using Sfa.Das.Console.ApplicationServices.Models;
using Sfa.Das.Console.Core.Domain.Model;
using SFA.DAS.EmployerUsers.Api.Client;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Web.Services;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Console.Infrastructure
{
    public sealed class EmployerUserRepository : IEmployerUserRepository
    {
        private const int PageSize = 10;
        private readonly IEmployerUsersApiClient _client;
        private readonly IMapUserSearchItems _mapper;
        private readonly ILog _logger;

        public EmployerUserRepository(ILog logger, IEmployerUsersApiClient client, IMapUserSearchItems mapper)
        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
        }

        public async Task<EmployerUserSearchResults> Search(string searchTerm, int page)
        {
            _logger.Debug($"IEmployerUsersApiClient.SearchEmployerUsers(\"{searchTerm}\",{page},{PageSize});");

            var response = await _client.SearchEmployerUsers(searchTerm, page, PageSize);

            var results = MapToEmployerUserSummary(response.Data).ToList();

            return new EmployerUserSearchResults
            {
                Page = response.Page,
                LastPage = response.TotalPages,
                Results = results
            };
        }

        public async Task<ResultPage<SearchItem>> FindPage(int limit, int start)
        {
            var pageNumber = start / limit + 1;
            var task = await _client.GetPageOfEmployerUsers(1, 3);

            var hasMorePages = task.TotalPages > task.Page;
            start = ((task.Page - 1) * limit) + 1;

            return new ResultPage<SearchItem>
            {
                Links = GenerateLinks(limit, start, hasMorePages),
                Results = task.Data.Select(_mapper.Map),
                Size = limit,
                Start = start
            };
        }

        private PageLinks GenerateLinks(int limit, int start, bool hasMore)
        {
            var baseUrl = "/api/manifest/user?limit={0}&start={1}";
            var links = new PageLinks
            {
                Self = string.Format(baseUrl, limit, start)
            };

            if (hasMore)
            {
                links.Next = string.Format(baseUrl, limit, start + limit);
            }

            if (start - limit > 0)
            {
                links.Prev = string.Format(baseUrl, limit, start - limit);
            }

            return links;
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

        private IEnumerable<EmployerUserSummary> MapToEmployerUserSummary(List<UserSummaryViewModel> data)
        {
            foreach (var userSummaryViewModel in data)
            {
                yield return new EmployerUserSummary
                {
                    Id = userSummaryViewModel.Id,
                    FirstName = userSummaryViewModel.FirstName,
                    LastName = userSummaryViewModel.LastName,
                    Email = userSummaryViewModel.Email,
                    IsLocked = userSummaryViewModel.IsLocked,
                    IsActive = userSummaryViewModel.IsActive,
                    Href = userSummaryViewModel.Href
                };
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
    }
}
