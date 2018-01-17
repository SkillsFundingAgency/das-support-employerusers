﻿using System.Diagnostics.CodeAnalysis;
using System.Web.Http;

namespace SFA.DAS.EmployerUsers.Support.Web
{
    [ExcludeFromCodeCoverage]
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            //config.MessageHandlers.Add( new TokenValidationHandler());
        }
    }
}