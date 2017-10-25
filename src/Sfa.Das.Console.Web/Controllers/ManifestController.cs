using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using ESFA.DAS.Support.Shared;
using Sfa.Das.Console.ApplicationServices;
using Sfa.Das.Console.Infrastructure;
using SFA.DAS.EmployerUsers.Support.Web.Services;

namespace Sfa.Das.Console.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/manifest")]
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
                ResourceUrlFormat = "/account/team/{0}",
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
