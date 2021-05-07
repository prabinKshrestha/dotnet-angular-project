using AT.Common.Exceptions;
using AT.Data.Interface;
using AT.Service.Settings.SiteSettings.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteSettingEntity = AT.Entity.Settings.SiteSettings.SiteSetting;

namespace AT.Service.Settings.Rule
{
    public class SiteSettingServiceRule : ISiteSettingServiceRule
    {
        private readonly IRepository<SiteSettingEntity> _siteSettingRepository;
        public SiteSettingServiceRule(IRepository<SiteSettingEntity> siteSettingRepository)
        {
            _siteSettingRepository = siteSettingRepository;
        }
        public void CheckAddRule(SiteSettingEntity entity)
        {
            throw new NotImplementedException();
        }

        public void CheckUpdateRule(SiteSettingEntity entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("Error While Updating SiteSettings.", validations);
            }
        }

        public void CheckDeleteRule(SiteSettingEntity entity)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<ATBusinessExceptionMessage> PerformBasicCheck(SiteSettingEntity entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
           
            if(string.IsNullOrWhiteSpace(entity.ImageName))

            {
                validations.Add(new ATBusinessExceptionMessage("Image cannot be empty", "Image"));   
            }
             return validations;
        }
    }
}
