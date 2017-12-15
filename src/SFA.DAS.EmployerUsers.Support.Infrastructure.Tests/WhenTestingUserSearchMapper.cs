using NUnit.Framework;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure.Tests
{
    [TestFixture]
    public class WhenTestingUserSearchMapper
    {

        private IMapUserSearchItems _unit;
        private SaerchUserModel _user;

        [SetUp]
        public void Setup()
        {
            _user = new SaerchUserModel()
            {
                Id = "USER100",
                Email = "someone@tempuri.org",
                FirstName = "Test",
                LastName = "User"
            };
            _unit = new UserSearchMapper();
        }

        [Test]
        public void ItShouldMapAUserToASearchItem()
        {
            var actual = _unit.Map(_user);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void ItShouldMapTheUserId()
        {
            var actual = _unit.Map(_user);
            Assert.AreEqual("USER100", actual.SearchId);

        }
        [Test]
        public void ItShouldMapThenItShouldMapSearchResultCategory()
        {
            var actual = _unit.Map(_user);
            Assert.AreEqual("USER", actual.SearchResultCategory);

        }

        [Test]
        public void ItShouldMapAtleastOneKeyword()
        {
            var actual = _unit.Map(_user);
            Assert.AreNotEqual(0, actual.Keywords.Length);

        }

    }
}