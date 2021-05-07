using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.System.Loggers
{
    public interface IATLogger
    {
        void LogExceptionToFile(ATExceptionTypes aTExceptionTypes
            , string message
            , Exception exception
            , ATErrorLevel aTErrorLevel = ATErrorLevel.Error
            , bool sendMessage = false);
    }
}
