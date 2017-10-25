namespace Sfa.Das.Console.Core.Configuration
{
    public interface IConfigurationSettings
    {
        string EnvironmentName { get; }

        string ApplicationName { get; }
    }
}
