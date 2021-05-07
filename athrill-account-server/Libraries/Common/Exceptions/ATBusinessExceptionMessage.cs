using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Exceptions
{
    public class ATBusinessExceptionMessage
    {
        public ATErrorLevel ErrorLevel { get; set; }
        public string Message { get; set; }
        public string TargetType { get; set; }
        public ATBusinessExceptionMessage(string message, string targetType = null)
        {
            ErrorLevel = ATErrorLevel.Error;
            Message = message;
            TargetType = targetType;
        }
        public ATBusinessExceptionMessage(ATErrorLevel level, string message, string targetType = null)
        {
            ErrorLevel = level;
            Message = message;
            TargetType = targetType;
        }
    }
}
