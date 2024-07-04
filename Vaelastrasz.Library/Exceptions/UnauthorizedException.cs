﻿using System;
using System.Collections.Generic;
using System.Text;

// 401
namespace Vaelastrasz.Library.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base() { }
        public UnauthorizedException(string message) : base(message) { }
        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
