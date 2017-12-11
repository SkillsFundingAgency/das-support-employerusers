using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.EmployerUsers.Support.Core.Domain.Model
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool IsLocked { get; set; }
    }
}