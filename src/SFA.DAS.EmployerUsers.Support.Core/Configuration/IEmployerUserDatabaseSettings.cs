namespace Sfa.Das.Console.Core.Configuration
{
    public interface IEmployerUserDatabaseSettings
    {
        string ConnectionString { get; }
    }

    public class EmployerUserDatabaseSettings : IEmployerUserDatabaseSettings
    {

        public string ConnectionString { get; }
    }
}