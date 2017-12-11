using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerUsers.Support.Core.Domain.Model;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using SFA.DAS.EmployerUsers.Support.Web.Controllers;

namespace SFA.DAS.EmployerUsers.Support.Web.Tests
{
    public class WhenTestingUserController
    {
        [SetUp]
        public void Setup()
        {
            _mockEmployerUserRepository = new Mock<IEmployerUserRepository>();
            _unit = new UserController(_mockEmployerUserRepository.Object);
        }

        protected UserController _unit;
        protected Mock<IEmployerUserRepository> _mockEmployerUserRepository;
        protected readonly string _id = "123";
        protected EmployerUser _user;
    }
}