using System;
using System.Collections.Generic;
using System.Text;

namespace Vaelastrasz.Library.Exceptions
{
    public class BadGatewayException : Exception
    {
        public BadGatewayException() : base()
        {
        }

        public BadGatewayException(string message) : base(message)
        {
        }

        public BadGatewayException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
