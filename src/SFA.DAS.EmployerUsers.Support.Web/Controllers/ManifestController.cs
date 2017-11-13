using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using ESFA.DAS.Support.Shared;
using Sfa.Das.Console.ApplicationServices;

namespace SFA.DAS.EmployerUsers.Support.Web.Controllers
{
    [RoutePrefix("api/manifest")]
    public class ManifestController : ApiController
    {
        private readonly IEmployerUserRepository _repository;

        public ManifestController(IEmployerUserRepository repository)
        {
            _repository = repository;
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
        public IEnumerable<SearchItem> User()
        {
            return _repository.FindAll();
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
                SearchItemsUrl = "/api/manifest/user"
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
