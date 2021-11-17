using System;

namespace Gateways.Core.Exceptions
{
    public class MaxDevicesException : ApplicationException
    {
        public MaxDevicesException() : base("Max allowed devices per Gateway reached.") { }

        public MaxDevicesException(string message) : base(message) { }

        public MaxDevicesException(string message, Exception innerException) : base(message, innerException) { }
    }
}
