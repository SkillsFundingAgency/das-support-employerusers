﻿using Sfa.Das.Console.Core.Services;
using SFA.DAS.EmployerUsers.Api.Client;

namespace Sfa.Das.Console.Infrastructure.Settings
{
    public class EmployerUsersApiConfiguration : IEmployerUsersApiConfiguration
    {
        private readonly IProvideSettings _settings;

        public EmployerUsersApiConfiguration(IProvideSettings settings)
        {
            _settings = settings;
        }

        public string ApiBaseUrl => _settings.GetSetting("EmpUserApiBaseUrl");
        public string ClientId => _settings.GetSetting("EmpUserApiClientId");
        public string ClientSecret => _settings.GetSetting("EmpUserApiClientSecret");
        public string IdentifierUri => _settings.GetSetting("EmpUserApiIdentifierUri");
        public string Tenant => _settings.GetSetting("EmpUserApiTenant");
        public string ClientCertificateThumbprint => _settings.GetNullableSetting("EmpUserApiCertificateThumbprint");
    }
}
