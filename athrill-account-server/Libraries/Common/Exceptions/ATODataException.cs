using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AT.Common.Exceptions
{
    public class ATODataException : Exception
    {
        public ATODataException() { }
        public ATODataException(string message) : base(message) { }
    }
}
