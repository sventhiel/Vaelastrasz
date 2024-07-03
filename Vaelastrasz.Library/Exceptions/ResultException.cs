using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Vaelastrasz.Library.Exceptions
{
    public class ResultException : Exception
    {
        public string ResultName { get; }
        public ResultException() { }

        public ResultException(string message)
            : base(message) { }

        public ResultException(string message, Exception innerException)
            : base(message, innerException) { }

        public ResultException(string message, string resultName)
            : base(message)
        {
            ResultName = resultName;
        }

        public override string ToString()
        {
            var resultInfo = ResultName == null ? "" : $" Result Name: {ResultName}";
            return base.ToString() + resultInfo;
        }
    }
}
