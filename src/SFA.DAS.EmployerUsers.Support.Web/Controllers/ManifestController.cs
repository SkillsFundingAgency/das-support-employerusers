using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using SFA.DAS.Support.Shared;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;
using SFA.DAS.EmployerUsers.Support.Infrastructure;

namespace SFA.DAS.EmployerUsers.Support.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/manifest")]
    public class ManifestController : ApiController
    {
        private readonly IEmployerUserHandler _handler;

        public ManifestController(IEmployerUserHandler handler)
        {
            _handler = handler;
        }


        [Route("")]
        public SiteManifest Get()
        {
            return new SiteManifest
            {
                Version = GetVersion(),
                Resources = GetResources(),
                SearchResultsMetadata = GetSearchResultsMetadata(),
                BaseUrl = Url.Content("~/")
            };
        }

        [HttpGet]
        public async Task<IEnumerable<SearchItem>> Search()
        {
            return await _handler.FindSearchItems();
        }

        private IEnumerable<SiteResource> GetResources()
        {
            return new List<SiteResource>()
            {

                new SiteResource
                {
                    ResourceKey = "account/team",
                    ResourceTitle = "Team members",
                    ResourceUrlFormat = "/account/team/{0}"
                },

                new SiteResource
                {
                    ResourceKey = "user/header",
                    ResourceUrlFormat = "/user/header/{0}"
                },
                new SiteResource
                {
                    ResourceKey = "user",
                    ResourceTitle = "Overview",
                    ResourceUrlFormat = "/user/index/{0}",
                    SearchItemsUrl = "/api/manifest/search"
                }
            };

        }

        private IEnumerable<SearchResultMetadata> GetSearchResultsMetadata()
        {
            return new List<SearchResultMetadata>()
            {

                new SearchResultMetadata
                {
                  SearchResultCategory = GlobalConstants.SearchResultCategory,
                  ColumnDefinitions = new List<SearchColumnDefinition>
                  {
                      new SearchColumnDefinition
                      {
                         Name = nameof(SearchUserModel.Id),
                         HideColumn = true
                      },
                      new SearchColumnDefinition
                      {
                          Name = nameof(SearchUserModel.Name),
                          Link = new LinkDefinition
                          {
                              Format = "/resource/?key=user&id={0}",
                              MapColumnName =  nameof(SearchUserModel.Id)
                          }
                      },
                      new SearchColumnDefinition
                      {
                          Name = nameof(SearchUserModel.Email),
                      },
                      new SearchColumnDefinition
                      {
                            Name = nameof(SearchUserModel.Status)
                      }
                  }

                }
            };

        }
        private string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}
