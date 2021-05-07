using AT.Entity.Settings.SiteSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Settings.SiteSettings
{
    public interface ISiteSettingService : IBaseService<SiteSetting>
    {
        SiteSetting GetSiteSetting();
    }
}
