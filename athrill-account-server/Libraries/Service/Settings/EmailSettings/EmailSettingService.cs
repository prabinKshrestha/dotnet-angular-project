using AT.Data.Interface;
using AT.Service.Settings.EmailSettings.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AT.Common.Exceptions;
using AT.Common.Helpers;
using Microsoft.Extensions.Configuration;
using AT.Entity.Settings.EmailSettings;
using AT.Common.Api.Request;
using AT.Data.Request;
using AT.Common.Api.Infrastructure;

namespace AT.Service.Settings.EmailSettings
{
    public class EmailSettingService : QueryService<EmailSetting>, IEmailSettingService
    {
        private readonly IRepository<EmailSetting> _emailSettingRepository;
        private readonly IEmailSettingServiceRule _emailSettingServiceRule;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public EmailSettingService(
            IRepository<EmailSetting> emailSettingRepository,
            IEmailSettingServiceRule emailSettingServiceRule,
            IUnitOfWork unitOfWork,
            IConfiguration configuration
        )
        {
            _emailSettingRepository = emailSettingRepository;
            _emailSettingServiceRule = emailSettingServiceRule;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        #region Modify

        public EmailSetting Get(int id)
        {
            return _emailSettingRepository.Get(id);
        }

        public IEnumerable<EmailSetting> GetAll()
        {
            return _emailSettingRepository.GetAll();
        }
        public override EmailSetting GetById(int id, GetByIdRequestBase request = null)
        {
            return GetQueryForId(request).FinalizeQueryById(x => x.EmailSettingId == id, id);
        }
        public EmailSetting GetDefaultEmailSetting()
        {
            return _emailSettingRepository.TableNotTracked.FirstOrDefault(x => x.IsDefault && x.IsPublished);
        }

        #endregion

        #region Modify

        public void Add(EmailSetting entity)
        {
            _emailSettingServiceRule.CheckAddRule(entity);
            PerformIsDefaultOperation(entity);
            entity.Password = SecurityHelper.Encrypt(entity.Password, _configuration.GetValue<string>("ATAppSettings:EmailSettingPassWordSecretKey"));
            _emailSettingRepository.Add(entity);
            _unitOfWork.Commit();
        }
        public void Update(EmailSetting entity)
        {
            _emailSettingServiceRule.CheckUpdateRule(entity);
            PerformIsDefaultOperation(entity);
            if (!_emailSettingRepository.TableNotTracked.Any(x => x.EmailSettingId == entity.EmailSettingId && x.Password == entity.Password))
            {
                entity.Password = SecurityHelper.Encrypt(entity.Password, _configuration.GetValue<string>("ATAppSettings:EmailSettingPassWordSecretKey"));
            }
            _emailSettingRepository.Update(entity);
            _unitOfWork.Commit();
        }
        public void Delete(EmailSetting entity)
        {
            _emailSettingServiceRule.CheckDeleteRule(entity);
            _emailSettingRepository.Delete(entity);
            _unitOfWork.Commit();
        }
        public void ChangeDefaultStatus(int emailId, bool defaultStatus)
        {
            EmailSetting emailSetting = _emailSettingRepository.TableNotTracked.FirstOrDefault(x => x.EmailSettingId == emailId);
            if (emailSetting != null)
            {
                if (defaultStatus != emailSetting.IsDefault)
                {
                    emailSetting.IsDefault = defaultStatus;
                    PerformIsDefaultOperation(emailSetting);
                    _emailSettingRepository.Update(emailSetting);
                    _unitOfWork.Commit();
                }
            }
            else
            {
                throw new ATReferenceException(emailId, typeof(EmailSetting).Name);
            }

        }
        private void PerformIsDefaultOperation(EmailSetting entity)
        {

            if (entity.IsDefault) // perform this for other record isdefault as false
            {
                EmailSetting defaultEmail = _emailSettingRepository.TableNotTracked.FirstOrDefault(x => x.EmailSettingId != entity.EmailSettingId && x.IsDefault == true);
                if (defaultEmail != null)
                {
                    defaultEmail.IsDefault = false;
                    _emailSettingRepository.Update(defaultEmail);
                }

            }
            else
            {
                if (!_emailSettingRepository.TableNotTracked.Any(x => x.IsDefault && x.EmailSettingId != entity.EmailSettingId))
                {
                    EmailSetting defaultEmail = _emailSettingRepository.TableNotTracked.FirstOrDefault(x => x.EmailSettingId != entity.EmailSettingId);
                    if (defaultEmail != null)
                    {
                        defaultEmail.IsDefault = true;
                        _emailSettingRepository.Update(defaultEmail);
                    }
                    else
                    {
                        throw new ATBusinessException("There Should be at least one email and set as default");
                    }
                }
            }
        }

        #endregion

        #region Override

        public override void SetRepository()
        {
            QueryRepository = _emailSettingRepository;
        }

        public override void SetDefaultOrderByItem()
        {
            DefaultOrderByItem = new OrderByItem("CreatedOn", "desc");
        }

        #endregion
    }
}
