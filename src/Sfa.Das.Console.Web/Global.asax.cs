using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Console.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ILog _logger;

        public MvcApplication()
        {
            _logger = DependencyResolver.Current.GetService<ILog>();
        }

        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            var logger = DependencyResolver.Current.GetService<ILog>();

            logger.Info("Starting Web Role");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            logger.Info("Web Role started");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            var logger = DependencyResolver.Current.GetService<ILog>();

            //if (ex is HttpException
            //    && ((HttpException)ex).GetHttpCode() != 404)
            //{
                logger.Error(ex, "App_Error");
            //}
        }
    }
}
