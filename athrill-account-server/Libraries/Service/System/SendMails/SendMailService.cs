using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Common.Helpers;
using AT.Common.Models;
using AT.Entity.Settings.EmailSettings;
using AT.Entity.Settings.SiteSettings;
using AT.Service.Settings.EmailSettings;
using AT.Service.Settings.SiteSettings;
using AT.Service.System.Loggers;
using AT.Service.System.Loggers.FileLoggers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AT.Service.System.SendMails
{
    public class SendMailService : ISendMailService
    {
        private readonly IConfiguration _configuration;
        private IFileLoggerService _fileLoggerService;
        private SiteSetting _siteSetting; //this is used as global varialble so should use it with precaution
        private readonly ISiteSettingService _siteSettingService;
        private EmailSetting _emailSetting; //this is used as global varialble so should use it with precaution
        private readonly IEmailSettingService _emailSettingService;
        private const string SiteSettingImagePath = "api/images/sitesetting";
        public SendMailService(IConfiguration configuration
            , IEmailSettingService emailSettingService
            , ISiteSettingService siteSettingService
            , IFileLoggerService fileLoggerService)
        {
            _configuration = configuration;
            _fileLoggerService = fileLoggerService;
            _siteSettingService = siteSettingService;
            _emailSettingService = emailSettingService;
        }
        public void SendMailToUser(SendMailModel sendMailModel, EmailSetting emailSetting = null)
        {
            if (string.IsNullOrWhiteSpace(sendMailModel.ToEmail))
            {
                throw new ATBusinessException("Send Mail : Email is not valid");
            }
            if (string.IsNullOrWhiteSpace(sendMailModel.ToName))
            {
                throw new ATBusinessException("Send Mail : Name should not be empty");
            }

            if (_configuration.GetValue<bool>("ATAppSettings:CanSendMail"))
            {
                try
                {
                    _siteSetting = _siteSettingService.GetSiteSetting();
                    if (emailSetting != null)
                    {
                        _emailSetting = emailSetting;
                    }
                    else
                    {
                        _emailSetting = _emailSettingService.GetDefaultEmailSetting();
                    }

                    string decryptedPassword = SecurityHelper.Decrypt(_emailSetting.Password, _configuration.GetValue<string>("ATAppSettings:EmailSettingPassWordSecretKey"));

                    sendMailModel.Subject += $" | {_siteSetting.Name}";

                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress(_emailSetting.SendFromName, _emailSetting.Email);
                    MailboxAddress to = new MailboxAddress(sendMailModel.ToName, sendMailModel.ToEmail);
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    if (sendMailModel.IsHtml)
                    {
                        bodyBuilder.HtmlBody = GetHtmlBody(sendMailModel);
                    }
                    bodyBuilder.TextBody = sendMailModel.TextBody;
                    if (sendMailModel.Attachments != null && sendMailModel.Attachments.Any())
                    {
                        sendMailModel.Attachments.ForEach(x =>
                        {
                            MemoryStream ms = new MemoryStream();
                            x.CopyTo(ms);
                            ms.Position = 0;
                            bodyBuilder.Attachments.Add(x.FileName, ms);
                        });
                    }

                    message.From.Add(from);
                    message.To.Add(to);
                    message.Subject = sendMailModel.Subject;
                    message.Body = bodyBuilder.ToMessageBody();

                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.Connect(_emailSetting.Host, _emailSetting.Port, _emailSetting.IsSSL);
                        smtpClient.Authenticate(_emailSetting.Email, decryptedPassword);
                        smtpClient.Send(message);
                        smtpClient.Disconnect(true);
                        smtpClient.Dispose();
                    }
                }
                catch (Exception exception)
                {
                    _fileLoggerService.LogException(ATExceptionTypes.Unkown
                                                            , "Error While Sending Email."
                                                            , exception
                                                            , ATErrorLevel.Error);
                }
            }
        }
        public void SendMailToSystem(SendMailModel sendMailModel, EmailSetting emailSetting = null)
        {
            if (_configuration.GetValue<bool>("ATAppSettings:CanSendMail"))
            {
                try
                {
                    _siteSetting = _siteSettingService.GetSiteSetting();
                    if (emailSetting != null)
                    {
                        _emailSetting = emailSetting;
                    }
                    else
                    {
                        _emailSetting = _emailSettingService.GetDefaultEmailSetting();
                    }

                    string decryptedPassword = SecurityHelper.Decrypt(_emailSetting.Password, _configuration.GetValue<string>("ATAppSettings:EmailSettingPassWordSecretKey"));

                    sendMailModel.Subject += $" | {_siteSetting.Name}";

                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress(_emailSetting.SendFromName, _emailSetting.Email);
                    MailboxAddress to = new MailboxAddress(_siteSetting.Name, _siteSetting.FeedbackEmail);
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    if (sendMailModel.IsHtml)
                    {
                        bodyBuilder.HtmlBody = GetHtmlBody(sendMailModel);
                    }
                    bodyBuilder.TextBody = sendMailModel.TextBody;
                    if (sendMailModel.Attachments != null && sendMailModel.Attachments.Any())
                    {
                        sendMailModel.Attachments.ForEach(x =>
                        {
                            MemoryStream ms = new MemoryStream();
                            x.CopyTo(ms);
                            ms.Position = 0;
                            bodyBuilder.Attachments.Add(x.FileName, ms);
                        });
                    }
                    message.From.Add(from);
                    message.To.Add(to);
                    message.Subject = sendMailModel.Subject;
                    message.Body = bodyBuilder.ToMessageBody();

                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.Connect(_emailSetting.Host, _emailSetting.Port, _emailSetting.IsSSL);
                        smtpClient.Authenticate(_emailSetting.Email, decryptedPassword);
                        smtpClient.Send(message);
                        smtpClient.Disconnect(true);
                        smtpClient.Dispose();
                    }
                }
                catch (Exception exception)
                {
                    _fileLoggerService.LogException(ATExceptionTypes.Unkown
                                                            , "Error While Sending Email."
                                                            , exception
                                                            , ATErrorLevel.Error);
                }
            }
        }

        private string GetHtmlBody(SendMailModel sendMailModel)
        {
            return $@"
<html>
<head></head>
<body style=""margin:0; padding:0;"">
    <table cellpadding=""0"" cellspacing=""0"" width=""70%"" style=""/* border-collapse:collapse; */margin:auto;border: 1px solid #ccc;box-shadow: 12px 12px 12px #ccc;border-radius: 18px;margin-top: 50px;"">     
        <tbody>
            <tr style=""border-bottom: 2px solid #ccc !important;"">    
                <td align=""center"" style=""padding: 20px 0 0px 0;"">
                    <img src=""{_siteSetting.SiteUrl}/{SiteSettingImagePath}/{_siteSetting.ImageName}"" alt=""{_siteSetting.SiteUrl}"" width=""104"" height=""142"" style=""display: block;"">               
                </td>
            </tr>
            <tr>
                <td style=""padding: 33px 30px 40px 30px;"">
                    <table cellpadding=""0"" cellspacing=""0"" width=""100%"">                     
                        <tbody>
                            <tr>
                                <td>
                                    { (!string.IsNullOrWhiteSpace(sendMailModel.Content?.Title) ? sendMailModel.Content?.Title : "")   }
                                </td>
                            </tr>
                            <tr>
                                <td style=""padding: 10px 0 0px 0;"">
                                    { GetHtmlEmailBodies(sendMailModel.Content) }
                                </td>
                            </tr>
                            <tr>
                                <td style=""padding: 10px 0 0px 0;"">
                                    { GetButtonHtmlElement(sendMailModel.Button) }
                                </td>                           
                            </tr>
                        </tbody>
                    </table>                           
                </td>                           
            </tr>                           
            <tr>                           
                <td bgcolor=""#ee4c50"" style=""border-bottom-left-radius: 17px;padding: 30px 30px 30px 30px;border-bottom-right-radius: 17px;"">                              
                    <table cellpadding=""0"" cellspacing=""0"" width=""100%"">                                     
                        <tbody>
                            <tr>                     
                                <td width=""75%"" style=""color: #fff;"">
                                        ® {_siteSetting.Name}, {_siteSetting.CopyrightName} {DateTime.UtcNow.Year} <br>
                                        {_siteSetting.AddressShortDetail}<br>
                                        <a href=""{_siteSetting.SiteUrl}"">{_siteSetting.SiteUrl}</a>
                                </td>                          
                                <td align=""right"">
                                    <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                                        <tbody>
                                            <tr>                 
                                                <td>                                                                                            
                                                    <a href=""{_siteSetting.FacebookLink}"">                                          
                                                        <img src=""{_siteSetting.SiteUrl}/{_configuration.GetValue<string>("ATExternal:FacebookImageUrl")}"" alt=""Twitter"" width=""38"" height=""38"" style=""display:block;"" border=""0"">                                                       
                                                    </a>                                                       
                                                </td>                                                     
                                                <td style=""font-size:0; line-height:0;"" width=""20""> &nbsp;</td>                                                              
                                                <td>               
                                                    <a href=""{_siteSetting.LinkedInLink}"">                                                               
                                                        <img src=""{_siteSetting.SiteUrl}/{_configuration.GetValue<string>("ATExternal:LinkedInImageUrl")}"" alt=""Facebook"" width=""38"" height=""38"" style=""display:block;"" border=""0"">                                                                         
                                                    </a>                                                                         
                                                </td>                                                                         
                                            </tr>                                                                         
                                        </tbody>
                                    </table>                                                                         
                                </td>                                                                       
                            </tr>                                                                        
                        </tbody>
                    </table>                                                                         
                </td>                                                                         
            </tr>                                                                         
        </tbody>
    </table>                                                                        
</body>
</html>
            ";
        }

        private string GetButtonHtmlElement(SendMailButtonModel buttonModel)
        {
            string retVal = string.Empty;
            if (buttonModel != null)
            {
                retVal = $@"
                            <a href=""{buttonModel.Url}"" style=""color:#fff"">
                                <button style=""background:#ee4c50;color: #fff;cursor: pointer;border: none;padding: 6px;border-radius: 6px;cursor:pointer;"">
                                    {buttonModel.Name}
                                </button>
                            </a>";
            }
            return retVal;          
        }

        private string GetHtmlEmailBodies(SendMailContentModel content)
        {
            string retVal = string.Empty;
            if(content?.Bodies != null && content.Bodies.Any())
            {
                content.Bodies.ForEach(body => retVal += $"<p>{body}</p>");
            }
            return retVal;
        }
    }
}
