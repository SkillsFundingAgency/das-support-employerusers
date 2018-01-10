using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerUsers.Api.Types;
using SFA.DAS.EmployerUsers.Support.Application.Handlers;
using SFA.DAS.EmployerUsers.Support.Infrastructure;
using SFA.DAS.EmployerUsers.Support.Core;

namespace SFA.DAS.EmployerUsers.Support.Application.Tests
{
    [TestFixture]
    public class WhenTestingEmployerUserHandler
    {
        [SetUp]
        public void Setup()
        {
            _employerUserRepository = new Mock<IEmployerUserRepository>();
            _unit = new EmployerUserHandler(_employerUserRepository.Object);
        }

        private IEmployerUserHandler _unit;
        private Mock<IEmployerUserRepository> _employerUserRepository;


        [Test]
        public async Task ItShouldFindSearchItems()
        {
            _employerUserRepository.Setup(x => x.FindAllDetails()).Returns(
                Task.FromResult(
                    new List<Core.Domain.Model.EmployerUser>
                    {
                        new Core.Domain.Model.EmployerUser
                        {
                            Email = "Someone@tempuri.org"
                        }
                    }.AsEnumerable()));

            var actual = await _unit.FindSearchItems();
            CollectionAssert.IsNotEmpty(actual);
        }
    }
}