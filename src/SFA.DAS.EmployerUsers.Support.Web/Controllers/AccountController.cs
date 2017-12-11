using System.Web.Mvc;

namespace SFA.DAS.EmployerUsers.Support.Web.Controllers
{
    public class AccountController : Controller
    {
      
        public ActionResult Team(string id)
        {
            if (Request.Url.Query.Contains("parent"))
            {
                return View((object)id);
            }

            var actionResult = View((object)id);
            actionResult.MasterName = "_Parent";
            return actionResult;
        }
    }
}