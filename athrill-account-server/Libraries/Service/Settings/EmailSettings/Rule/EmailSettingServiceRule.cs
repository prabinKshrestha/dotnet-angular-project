using AT.Common.Exceptions;
using AT.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmailSettingEntity = AT.Entity.Settings.EmailSettings.EmailSetting;

namespace AT.Service.Settings.EmailSettings.Rule
{
    public class EmailSettingServiceRule : IEmailSettingServiceRule
    {
        private readonly IRepository<EmailSettingEntity> _emailSettingRepository;
        public EmailSettingServiceRule(IRepository<EmailSettingEntity> emailSettingRepository)
        {
            _emailSettingRepository = emailSettingRepository;
        }

        public void CheckAddRule(EmailSettingEntity entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("EmailSetting Business Rule Violations", validations);
            }
        }

        public void CheckUpdateRule(EmailSettingEntity entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("EmailSetting Business Rule Violations", validations);
            }
        }

        public void CheckDeleteRule(EmailSettingEntity entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();

            if (_emailSettingRepository.TableNotTracked.Count() == 1)
            {
                validations.Add(new ATBusinessExceptionMessage("There should be more than one record in order to delete.","Count"));
            }
            if(entity.IsDefault)
            {
                validations.Add(new ATBusinessExceptionMessage("This is a default record. First make other record default then delete this.","Default Status"));
            }
            if (validations.Any())
            {
                throw new ATBusinessException("EmailSetting Business Rule Violations", validations);
            }


        }

        private IEnumerable<ATBusinessExceptionMessage> PerformBasicCheck(EmailSettingEntity entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            List<EmailSettingEntity> existingDatas = _emailSettingRepository.TableNotTracked.ToList();

            if (string.IsNullOrWhiteSpace(entity.Password))
            {
                validations.Add(new ATBusinessExceptionMessage("Password cannot be empty", "Password"));
            }                
            return validations;
        }
    }
}
