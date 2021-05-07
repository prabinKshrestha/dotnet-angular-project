using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Exceptions
{
    public class ATAuthenticationException : Exception
    {
        public ATAuthenticationException() { }
        public ATAuthenticationException(string message) : base(message) { }
    }
}
