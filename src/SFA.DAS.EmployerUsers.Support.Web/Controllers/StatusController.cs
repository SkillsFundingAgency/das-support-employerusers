using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SFA.DAS.EmployerUsers.Support.Web.Controllers
{
    [RoutePrefix("api/status")]
    public class StatusController : ApiController
    {
        // GET: Status
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new
            {
                ServiceName = AddServiceName(),
                ServiceTime = AddServerTime(),
                Request = AddRequestContext()
            });
        }

        private string AddRequestContext()
        {
            try
            {
                return $" {HttpContext.Current.Request.HttpMethod}: {HttpContext.Current.Request.RawUrl}";
            }
            catch
            {
                return "Unknown";
            }
        }

        private DateTimeOffset AddServerTime()
        {
            return DateTimeOffset.UtcNow;
        }

        private string AddServiceName()
        {
            try
            {
                return "SFA DAS Employer Users Support Site";
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}