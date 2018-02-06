using System.Threading.Tasks;
using System.Web.Http;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;

namespace SFA.DAS.EmployerUsers.Support.Web.Controllers
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        private readonly IEmployerUserHandler _handler;

        public SearchController(IEmployerUserHandler handler)
        {
            _handler = handler;
        }


        [HttpGet]
        public async Task<IHttpActionResult> Users()
        {
            var model = await _handler.FindSearchItems();
            return Json(model);
        }

    }
}