using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Exceptions
{
    public class ATBusinessExceptionModel
    {
        public List<ATBusinessExceptionMessageModel> Validations { get; set; }
        public string Message { get; set; }
    }
    public class ATBusinessExceptionMessageModel
    {
        public ATErrorLevel ErrorLevel { get; set; }
        public string Message { get; set; }
        public string TargetType { get; set; }
    }
}
