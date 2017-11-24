using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using ESFA.DAS.Support.Shared;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;
using SFA.DAS.EmployerUsers.Support.Infrastructure;

namespace SFA.DAS.EmployerUsers.Support.Web.Controllers
{
    [RoutePrefix("api/manifest")]
    public class ManifestController : ApiController
    {
        private IEmployerUserHandler _handler;

        public ManifestController(IEmployerUserHandler handler)
        {
            _handler = handler;
        }


        [System.Web.Http.Route("")]
        public SiteManifest Get()
        {
            return new SiteManifest
            {
                Version = GetVersion(),
                Resources = GetResources(),
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
            yield return new SiteResource
            {
                ResourceKey = "account/team",
                ResourceTitle = "Team members",
                ResourceUrlFormat = "/account/team/{0}"
            };

            yield return new SiteResource
            {
                ResourceKey = "user/header",
                ResourceUrlFormat = "/user/header/{0}"
            };

            yield return new SiteResource
            {
                ResourceKey = "user",
                ResourceTitle = "Overview",
                ResourceUrlFormat = "/user/index/{0}",
                SearchItemsUrl = "/api/manifest/search"
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
