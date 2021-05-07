using AT.Common.Models;
using AT.Entity.Settings.EmailSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.System.SendMails
{
    public interface ISendMailService
    {
        void SendMailToUser(SendMailModel sendMailModel, EmailSetting emailSetting = null);
        void SendMailToSystem(SendMailModel sendMailModel, EmailSetting emailSetting = null);
    }
}
