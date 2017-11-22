﻿using System.Web;
using SFA.DAS.EmployerUsers.Support.Web.Logging;
using SFA.DAS.NLog.Logger;
using StructureMap.Configuration.DSL;

namespace SFA.DAS.EmployerUsers.Support.Web.DependencyResolution
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            For<IRequestContext>().Use(x => new RequestContext(new HttpContextWrapper(HttpContext.Current)));

        }
    }
}