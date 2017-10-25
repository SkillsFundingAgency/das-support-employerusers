using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure;
using System.Configuration;
using Sfa.Das.Console.Core.Configuration;

namespace Sfa.Das.Console.Infrastructure.Settings
{
    public sealed class ApplicationSettings : IConfigurationSettings
    {
        public string EnvironmentName => ConfigurationManager.AppSettings["EnvironmentName"];

        public string ApplicationName => ConfigurationManager.AppSettings["ApplicationName"];
    }
}
