using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using NUnit.Framework;
using SFA.DAS.EmployerUsers.Support.Web.Configuration;

namespace SFA.DAS.EmployerUsers.Support.Web.Tests.Configuration
{
    [TestFixture]
    public class WebConfigurationTesting
    {
        private const string SiteConfigFileName = "SFA.DAS.Support.EmployerUsers";
        [SetUp]
        public void Setup()
        {
            _unit = new WebConfiguration
            {
                EmployerUsersApi = new Web.Configuration.EmployerUsersApiConfiguration()
                {
                    ApiBaseUrl = "--- configuration value goes here ---",
                    ClientId = "00000000-0000-0000-0000-000000000000",
                    ClientSecret = "--- configuration value goes here ---",
                    IdentifierUri = "--- configuration value goes here ---",
                    Tenant = "--- configuration value goes here ---",
                    ClientCertificateThumbprint = "--- configuration value goes here ---"
                }
            };
        }

        private WebConfiguration _unit;

        [Test]
        public void ItShouldSerialize()
        {
            var json = JsonConvert.SerializeObject(_unit);
            Assert.IsFalse(string.IsNullOrWhiteSpace(json));


            System.IO.File.WriteAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\{SiteConfigFileName}.json", json);

        }

        [Test]
        public void ItShouldDeserialize()
        {
            var json = JsonConvert.SerializeObject(_unit);
            Assert.IsNotNull(json);
            var actual = JsonConvert.DeserializeObject<WebConfiguration>(json);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void ItShouldDeserialiseFaithfuly()
        {
            var json = JsonConvert.SerializeObject(_unit);
            Assert.IsNotNull(json);
            var actual = JsonConvert.DeserializeObject<WebConfiguration>(json);
            Assert.AreEqual(json, JsonConvert.SerializeObject(actual));
        }


        [Test]
        public void ItShouldGenerateASchema()
        {

            var provider = new FormatSchemaProvider();
            var jSchemaGenerator = new JSchemaGenerator();
            jSchemaGenerator.GenerationProviders.Clear();
            jSchemaGenerator.GenerationProviders.Add(provider);
            var actual = jSchemaGenerator.Generate(typeof(WebConfiguration));

            
            Assert.IsNotNull(actual);
            // hack to leverage format as 'environmentVariable'
            var schemaString = actual.ToString().Replace($"\"format\":", "\"environmentVariable\":");
            Assert.IsNotNull(schemaString);
            System.IO.File.WriteAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\{SiteConfigFileName}.schema.json", schemaString);
        }
    }
}