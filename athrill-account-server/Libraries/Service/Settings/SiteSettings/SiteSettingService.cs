using AT.Common.Enum;
using AT.Data.Interface;
using AT.Service.Settings.SiteSettings.Rule;
using System;
using System.Collections.Generic;
using System.Text;
using SiteSettingEntity = AT.Entity.Settings.SiteSettings.SiteSetting;

namespace AT.Service.Settings.SiteSettings
{
    public class SiteSettingService : ISiteSettingService
    {
        private readonly IRepository<SiteSettingEntity> _siteSettingRepository;
        private readonly ISiteSettingServiceRule _siteSettingServiceRule;
        private readonly IUnitOfWork _unitOfWork;

        public SiteSettingService(IRepository<SiteSettingEntity> siteSettingRepository, ISiteSettingServiceRule siteSettingServiceRule, IUnitOfWork unitOfWork)
        {
            _siteSettingRepository = siteSettingRepository;
            _siteSettingServiceRule = siteSettingServiceRule;
            _unitOfWork = unitOfWork;
        }

        public SiteSettingEntity Get(int id)
        {
            return _siteSettingRepository.Get(id);
        }
        public SiteSettingEntity GetSiteSetting()
        {
            return _siteSettingRepository.Get((int)ATSettings.SiteSettingId);
        }
        public IEnumerable<SiteSettingEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        #region Modify Methods

        public void Add(SiteSettingEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SiteSettingEntity entity)
        {
            _siteSettingServiceRule.CheckUpdateRule(entity);
            _siteSettingRepository.Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(SiteSettingEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
