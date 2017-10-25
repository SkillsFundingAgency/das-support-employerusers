using System;

namespace Sfa.Das.Console.Core.Domain.Exceptions
{
    public class EntityNotFoundException : ApplicationException {
        public object Identifier { get; set; }

        public EntityNotFoundException(string message, object identifier, Exception innerException) : base(message, innerException)
        {
            Identifier = identifier;
        }
    }
}