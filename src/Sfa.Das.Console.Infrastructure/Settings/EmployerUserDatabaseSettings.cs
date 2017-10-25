using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sfa.Das.Console.Core.Configuration;
using Sfa.Das.Console.Core.Services;

namespace Sfa.Das.Console.Infrastructure.Settings
{
    public class EmployerUserDatabaseSettings : IEmployerUserDatabaseSettings
    {
        private readonly IProvideSettings _settings;

        public EmployerUserDatabaseSettings(IProvideSettings settings)
        {
            _settings = settings;
        }

        public string ConnectionString => _settings.GetSetting("EmpUserConnectionString");
    }
}
