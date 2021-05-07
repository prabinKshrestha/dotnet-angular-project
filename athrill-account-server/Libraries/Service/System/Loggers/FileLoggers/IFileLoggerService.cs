using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.System.Loggers.FileLoggers
{
    public interface IFileLoggerService
    {
        Tuple<string> LogException(ATExceptionTypes aTExceptionTypes
            , string message
            , Exception exception
            , ATErrorLevel aTErrorLevel = ATErrorLevel.Error);
    }
}
