using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sfa.Das.Console.Web.Logging;
using SFA.DAS.NLog.Logger;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Sfa.Das.Console.Web.DependencyResolution
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            For<IRequestContext>().Use(x => new RequestContext(new HttpContextWrapper(HttpContext.Current)));

        }
    }
}