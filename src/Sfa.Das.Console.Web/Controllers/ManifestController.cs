using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using ESFA.DAS.Support.Shared;
using MediatR;
using Sfa.Das.Console.ApplicationServices.Models.Requests;
using Sfa.Das.Console.Infrastructure;
using SFA.DAS.EmployerUsers.Support.Web.Services;

namespace Sfa.Das.Console.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/manifest")]
    public class ManifestController : ApiController
    {
        private readonly IMediator _mediator;

        public ManifestController(IMediator mediator)
        {
            _mediator = mediator;
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
        public ResultPage<SearchItem> User(int limit = 10, int start = 1)
        {
            if (start <= 0)
            {
                start = 1;
            }

            if (limit <= 0)
            {
                limit = 10;
            }

            var task = _mediator.SendAsync<EmployerUserPageResponse>(new EmployerUserPageRequest {Limit = limit, Start = start});
            Task.WaitAll(task);

            return task.Result.Page;
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
