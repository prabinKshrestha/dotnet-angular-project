using AT.Common.Enum;
using AT.Common.Helpers;
using AT.Common.Infrastructure.Interfaces;
using AT.Common.Models;
using AT.Service.System.SendMails;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AT.Service.System.Loggers.FileLoggers
{
    public class FileLoggerService : IFileLoggerService
    {
        private string _mainLogFolder;
        private readonly IClientInfoContext _clientInfoContext;
        public FileLoggerService(IConfiguration configuration
            , IClientInfoContext clientInfoContext)
        {
            _mainLogFolder = configuration.GetValue<string>("ATAppSettings:LogFolderName");
            _clientInfoContext = clientInfoContext;
        }
        public Tuple<string> LogException(ATExceptionTypes aTExceptionTypes
            , string message
            , Exception exception
            , ATErrorLevel aTErrorLevel = ATErrorLevel.Error)
        {
            string logDirectory = GetDirectoryToLog();
            string logFileName = string.Format("{0}__{1}__{2}.txt", DateTime.UtcNow.ToString("dd"), DateTime.UtcNow.ToString("HH.mm.ss.ffff"), aTExceptionTypes.ToString());
            string logFullPath = Path.Combine(logDirectory, logFileName);
            string exceptionToLog = CreateExceptionLogMessage(aTExceptionTypes, message, exception, aTErrorLevel);
            using (StreamWriter sw = File.CreateText(logFullPath))
            {
                sw.WriteLine(exceptionToLog);
            }
            return new Tuple<string>(exceptionToLog);
        }

        private string CreateExceptionLogMessage(ATExceptionTypes aTExceptionTypes, string message, Exception exception, ATErrorLevel aTErrorLevel)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(aTErrorLevel.ToString());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Exception : {aTExceptionTypes.GetDisplayName()}");
            if (aTExceptionTypes == ATExceptionTypes.Unkown && exception != null)
            {
                stringBuilder.AppendLine($"Exception Type: {exception.GetType().Name}");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Message : {message}");
            stringBuilder.AppendLine();
            if (exception != null)
            {
                stringBuilder.AppendLine($"Exception Message : {exception.Message}");
                stringBuilder.AppendLine($"Inner Exception Message : {exception.InnerException?.Message}");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"Stack Trace : ");
                stringBuilder.AppendLine($"{exception.StackTrace}");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine($"IpAddress : {_clientInfoContext.IPAddress}");
                stringBuilder.AppendLine(_clientInfoContext.GetClientInfoName());
                stringBuilder.AppendLine($"Route URL: {_clientInfoContext.RequestUrl}");
            }
            return stringBuilder.ToString();
        }

        private string GetDirectoryToLog()
        {
            string mainLogFolder = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), _mainLogFolder);
            string monthFolderToLog = $"{DateTime.UtcNow.Year}\\{DateTime.UtcNow.ToString("MM")}";
            string logPathToFolder = string.Format("{0}\\{1}", mainLogFolder, monthFolderToLog);
            Directory.CreateDirectory(logPathToFolder);
            return logPathToFolder;
        }
    }
}
