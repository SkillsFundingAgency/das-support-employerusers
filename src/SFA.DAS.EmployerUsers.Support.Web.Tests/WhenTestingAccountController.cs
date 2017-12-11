using System;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerUsers.Support.Web.Controllers;
using System.Web;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace SFA.DAS.EmployerUsers.Support.Web.Tests
{
    [TestFixture]
    public class WhenTestingAccountController
    {
        private AccountController _unit;
        private string _id = "123";
        private Mock<HttpContextBase> _mockContextBase;
        private ControllerContext _unitControllerContext;
        private Mock<HttpRequestBase> _mockRequestBase;
        private Mock<HttpResponseBase> _mockResponseBase;
        private Mock<IPrincipal> _mockUser;
        private RouteData _routeData;

        [SetUp]
        public void Setup()
        {
            _unit = new AccountController();

            _routeData = new RouteData();
            _mockContextBase = new Mock<HttpContextBase>();

            _mockRequestBase = new Mock<HttpRequestBase>();
            _mockResponseBase = new Mock<HttpResponseBase>();
            _mockUser = new Mock<IPrincipal>();

            _mockContextBase.Setup(x => x.Request).Returns(_mockRequestBase.Object);
            _mockContextBase.Setup(x => x.Response).Returns(_mockResponseBase.Object);
            _mockContextBase.Setup(x => x.User).Returns(_mockUser.Object);
            _unitControllerContext = new ControllerContext(_mockContextBase.Object, _routeData, _unit);

            _unit.ControllerContext = _unitControllerContext;
            
        }


        [Test]
        public void ItShouldReturnTheTeamParentViewIfTheParentParametersIsPassedInTheQuery()
        {
            _mockContextBase.Setup(x => x.Request.Url).Returns(new Uri("https://tempuri.org?parent"));
            var actual = _unit.Team(_id);
            Assert.IsInstanceOf<ViewResult>(actual);
            Assert.AreEqual(string.Empty, (actual as ViewResult).MasterName);
        }

        [Test]
        public void ItShouldReturnTheTeamChildViewIfTheParentParametersIsNotPassedInTheQuery()
        {
            _mockContextBase.Setup(x => x.Request.Url).Returns(new Uri("https://tempuri.org"));
            var actual = _unit.Team(_id);
            Assert.IsInstanceOf<ViewResult>(actual);
            Assert.AreEqual("_Parent", (actual as ViewResult).MasterName);

        }
    }
}