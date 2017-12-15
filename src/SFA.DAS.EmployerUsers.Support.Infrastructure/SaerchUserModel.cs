using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.EmployerUsers.Support.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class SaerchUserModel
    {
        public string Id { get; set; }
        public string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string Email { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}