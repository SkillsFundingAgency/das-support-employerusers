using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SFA.DAS.Support.Shared;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;
using SFA.DAS.EmployerUsers.Support.Web.Controllers;

namespace SFA.DAS.EmployerUsers.Support.Web.Tests
{
    [TestFixture]
    public class WhenTestingManifestController
    {
        [SetUp]
        public void Setup()
        {
            _employerUserHandler = new Mock<IEmployerUserHandler>();
            _unit = new ManifestController(_employerUserHandler.Object);


            _urlHelper = new Mock<System.Web.Http.Routing.UrlHelper>();
            _unit.Url = _urlHelper.Object;
            _urlHelper.Setup(x => x.Content(It.IsAny<string>())).Returns(@"~\");
        }

        private ManifestController _unit;
        private Mock<IEmployerUserHandler> _employerUserHandler;
        private Mock<System.Web.Http.Routing.UrlHelper> _urlHelper;

        [Test]
        public async Task ItShouldReturnAllOfTheSearchItems()
        {
            _employerUserHandler.Setup(x => x.FindSearchItems()).Returns(Task.FromResult(new List<SearchItem>
            {
                new SearchItem {Html = "", Keywords = new[] {"", ""}, SearchId = "123"}
            }.AsEnumerable()));
            var actual = await _unit.Search();
            CollectionAssert.IsNotEmpty(actual);
        }
        

        [Test]
        public void ItShouldReturnTheSiteManifest()
        {
            var actual = _unit.Get();
            Assert.IsNotNull(actual);
        }

        [Test]
        public void ItShouldReturnTheSiteManifestContaintResources()
        {
            var actual = _unit.Get();
            Assert.IsNotEmpty(actual.Resources);
            Assert.IsNotNull(actual.Resources.FirstOrDefault(x => x.ResourceKey == "account/team"));
            Assert.IsNotNull(actual.Resources.FirstOrDefault(x => x.ResourceKey == "user/header"));
            Assert.IsNotNull(actual.Resources.FirstOrDefault(x => x.ResourceKey == "user"));

        }

        [Test]
        public void ItShouldReturnTheSiteManifestHavingABaseUrl()
        {
            var actual = _unit.Get();
            Assert.IsNotNull(actual.BaseUrl);
        }

        [Test]
        public void ItShouldReturnTheSiteManifestHAvingAVersion()
        {
            var actual = _unit.Get();
            Assert.IsNotNull(actual.Version);
        }
    }
}