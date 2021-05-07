using AT.Entity.Settings.EmailSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Settings.EmailSettings
{
    public interface IEmailSettingService : IBaseService<EmailSetting>, IQueryService<EmailSetting>
    {
        void ChangeDefaultStatus(int emailId, bool defaultStatus);
        EmailSetting GetDefaultEmailSetting();
    }
}
