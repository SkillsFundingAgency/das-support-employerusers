using NUnit.Framework;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure.Tests
{
    [TestFixture]
    public class WhenTestingUserSearchMapper
    {

        private IMapUserSearchItems _unit;
        private User _user;

        [SetUp]
        public void Setup()
        {
            _user = new User(){Email = "someone@tempuri.org", FirstName = "Test", LastName = "User"};
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
            Assert.AreEqual($"USER-{_user.Id}", actual.SearchId);

        }
        [Test]
        public void ItShouldMapAnHtmlLinkContainingUserLookupUri()
        {
            var actual = _unit.Map(_user);
            Assert.IsTrue( actual.Html.Contains($"?key=user&id={_user.Id}"));

        }

        [Test]
        public void ItShouldMapAtleastOneKeyword()
        {
            var actual = _unit.Map(_user);
            Assert.AreNotEqual(0, actual.Keywords.Length);

        }

    }
}