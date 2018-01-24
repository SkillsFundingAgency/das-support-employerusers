using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;
using SFA.DAS.EmployerUsers.Support.Web.Controllers;
using SFA.DAS.Support.Shared.Discovery;
using SFA.DAS.Support.Shared.SearchIndexModel;

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


            _urlHelper = new Mock<UrlHelper>();
            _unit.Url = _urlHelper.Object;
            _urlHelper.Setup(x => x.Content(It.IsAny<string>())).Returns(@"~\");
        }

        private ManifestController _unit;
        private Mock<IEmployerUserHandler> _employerUserHandler;
        private Mock<UrlHelper> _urlHelper;

        [Test]
        public async Task ItShouldReturnAllOfTheSearchItems()
        {
            _employerUserHandler.Setup(x => x.FindSearchItems()).Returns(Task.FromResult(
                new List<UserSearchModel>
                {
                    new UserSearchModel
                    {
                        Email = "user@email.com"
                    }
                }.AsEnumerable()));


            var actual = await _unit.Search();

            Assert.IsNotNull(actual);

            var result = (JsonResult<IEnumerable<UserSearchModel>>) actual;

            CollectionAssert.IsNotEmpty(result.Content);
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
            var result = _unit.Get();

            Assert.IsNotNull(result);
            var actual = ((JsonResult<SiteManifest>) result).Content;

            Assert.IsNotEmpty(actual.Resources);
            Assert.IsNotNull(actual.Resources.FirstOrDefault(x => x.ResourceKey == "account/team"));
            Assert.IsNotNull(actual.Resources.FirstOrDefault(x => x.ResourceKey == "user/header"));
            Assert.IsNotNull(actual.Resources.FirstOrDefault(x => x.ResourceKey == "user"));
        }

        [Test]
        public void ItShouldReturnTheSiteManifestHavingABaseUrl()
        {
            var result = _unit.Get();
            Assert.IsNotNull(result);
            var actual = ((JsonResult<SiteManifest>) result).Content;
            Assert.IsNotNull(actual.BaseUrl);
        }

        [Test]
        public void ItShouldReturnTheSiteManifestHAvingAVersion()
        {
            var result = _unit.Get();
            Assert.IsNotNull(result);
            var actual = ((JsonResult<SiteManifest>) result).Content;
            Assert.IsNotNull(actual.Version);
        }
    }
}