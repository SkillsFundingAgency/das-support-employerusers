using System.Collections.Generic;

namespace Sfa.Das.Console.Infrastructure.DependencyResolution
{
    public interface ILoggingPropertyFactory
    {
        IDictionary<string, object> GetProperties();
    }
}