using System;

namespace Notifications.Domain.Exceptions
{
    public class ExternalEntitiesException : Exception
    {
        public ExternalEntitiesException()
        { }

        public ExternalEntitiesException(string message)
            : base(message)
        { }

        public ExternalEntitiesException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
