using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Sfa.Das.Console.ApplicationServices;
using SFA.DAS.EAS.Account.Api.Types;
using SFA.DAS.EmployerUsers.Api.Types;

namespace SFA.DAS.EmployerUsers.Support.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IEmployerUserRepository _repository;

        public UserController(IEmployerUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult> Header(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new BadRequestException();
            }

            var response = await _repository.Get(id);

            if (response == null)
            {
                return HttpNotFound();
            }
            return View("SubHeader", response);
        }

        public async Task<ActionResult> Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new BadRequestException();
            }

            var response = await _repository.Get(id);

            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }
    }
}