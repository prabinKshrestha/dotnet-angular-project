using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Exceptions
{
    public class ATBusinessException : Exception
    {
        public ATBusinessException() { }
        public ATBusinessException(string message) : base(message) { }
        public List<ATBusinessExceptionMessage> Validations { get; set; }
        public ATBusinessException(string message, List<ATBusinessExceptionMessage> validations) : base(message) { Validations = validations; }
    }
}
