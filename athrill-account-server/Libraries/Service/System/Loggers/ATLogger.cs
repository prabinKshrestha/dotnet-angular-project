using AT.Common.Enum;
using AT.Common.Models;
using AT.Service.System.Loggers.FileLoggers;
using AT.Service.System.SendMails;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AT.Service.System.Loggers
{
    public class ATLogger : IATLogger
    {
        private readonly ISendMailService _sendMailService;
        private readonly IFileLoggerService _fileLoggerService;
        private readonly IConfiguration _configuration;
        public ATLogger(ISendMailService sendMailService
            , IFileLoggerService fileLoggerService
            , IConfiguration configuration)
        {
            _sendMailService = sendMailService;
            _fileLoggerService = fileLoggerService;
            _configuration = configuration;
        }

        public void LogExceptionToFile(ATExceptionTypes aTExceptionTypes
            , string message
            , Exception exception
            , ATErrorLevel aTErrorLevel = ATErrorLevel.Error
            , bool sendMessage = false)
        {
            Tuple<string> tupleReturned =  _fileLoggerService.LogException(aTExceptionTypes, message, exception, aTErrorLevel);

            if (sendMessage && _configuration.GetValue<bool>("ATAppSettings:LogNotifyToDeveloper"))
            {
                string exceptionToLog = tupleReturned.Item1;

                string developerTeamEmail = _configuration.GetValue<string>("ATDevelopers:Email");
                string developerTeamName = _configuration.GetValue<string>("ATDevelopers:Name");
                string appNameForDeveloperTeam = _configuration.GetValue<string>("ATDevelopers:AppNameForDevelopers");

                StringBuilder exceptionMessageToMail = new StringBuilder();
                exceptionMessageToMail.AppendLine(exceptionToLog);
                exceptionMessageToMail.AppendLine();
                exceptionMessageToMail.AppendLine($"Application : {appNameForDeveloperTeam}");
                _sendMailService.SendMailToUser(new SendMailModel()
                {
                    ToEmail = developerTeamEmail,
                    ToName = developerTeamName,
                    Subject = "Error Log To Developer Team",
                    TextBody = exceptionMessageToMail.ToString(),
                    IsHtml = true,
                    Content = new SendMailContentModel()
                    {
                        Title = $"Dear {developerTeamName},",
                        Bodies = new List<string>() { exceptionMessageToMail.ToString() }
                    }
                });
            }
        }
    }
}
