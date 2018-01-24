using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using SFA.DAS.Support.Shared;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;
using SFA.DAS.Support.Shared.Discovery;
using SFA.DAS.Support.Shared.SearchIndexModel;

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
        public IHttpActionResult Get()
        {
            return Json(new SiteManifest
            {
                Version = GetVersion(),
                Resources = GetResources(),
                BaseUrl = Url.Content("~/")
            });
        }

        [HttpGet]
        public async Task<IHttpActionResult> Search()
        {
            var model = await _handler.FindSearchItems();
            return Json(model);
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
                    SearchItemsUrl = "/api/manifest/search",
                    SearchCategory = SearchCategory.User
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
