using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Common.Helpers;
using AT.Data.Interface;
using AT.Entity.Contents;
using AT.Entity.System.ATDatas;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Contents.Rule
{
    public class ContentServiceRule : IContentServiceRule
    {
        private readonly IRepository<Content> _contentRepository;
        private readonly IRepository<ContentType> _contentTypeRepository;
        private readonly IRepository<ATDataValue> _atDataValueRepository;

        public ContentServiceRule(IRepository<Content> contentRepository
            , IRepository<ContentType> contentTypeRepository
            , IRepository<ATDataValue> atDataValueRepository)
        {
            _contentRepository = contentRepository;
            _contentTypeRepository = contentTypeRepository;
            _atDataValueRepository = atDataValueRepository;
        }
        public void CheckAddRule(Content entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("Content Business Rule Violations.", validations);
            }
        }

        public void CheckDeleteRule(Content entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            if (_contentRepository.TableNotTracked.Any(x => x.ParentId == entity.ContentId))
            {
                validations.Add(new ATBusinessExceptionMessage("Content with child cannot be deleted.", "Content"));
            }
            if (validations.Any())
            {
                throw new ATBusinessException("Content Business Rule Violations.", validations);
            }
        }

        public void CheckUpdateRule(Content entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            validations.AddRange(PerformBasicCheck(entity));
            if (validations.Any())
            {
                throw new ATBusinessException("Content Business Rule Violations", validations);
            }
        }

        private IEnumerable<ATBusinessExceptionMessage> PerformBasicCheck(Content entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();

            List<Content> exsitingContents = _contentRepository.GetAll().ToList();

            if(!_contentTypeRepository.VerifyByReference(entity.ContentTypeId))
            {
                validations.Add(new ATBusinessExceptionMessage("Not valid Content Type.", "Content Type"));
            }
            if (!_atDataValueRepository.VerifyByReferenceForATDataValues(entity.PlacementId, ATDataTypes.ContentPlacement))
            {
                validations.Add(new ATBusinessExceptionMessage("Not valid Placement.", "Placement"));
            }
            if (entity.ParentId != null && !_contentRepository.VerifyByReference((int)entity.ParentId))
            {
                validations.Add(new ATBusinessExceptionMessage("Parent cannot be found.", "Parent"));
            }
            if (exsitingContents.Any(x => entity.ContentId == entity.ParentId))
            {
                validations.Add(new ATBusinessExceptionMessage(ATErrorLevel.Error, "Child cannot be parent.", "Parent"));
            }
            if (exsitingContents.Any(x => string.Equals(x.Name, entity.Name,StringComparison.OrdinalIgnoreCase) && entity.ContentId != x.ContentId))
            {
                validations.Add(new ATBusinessExceptionMessage(ATErrorLevel.Error, "Content Name Cannot be duplicated.", "Content Name"));
            }
            if (!string.IsNullOrEmpty(entity.Slug))
            {
                if (exsitingContents.Any(x => x.Slug == entity.Slug && x.ContentId != entity.ContentId))
                {
                    validations.Add(new ATBusinessExceptionMessage(ATErrorLevel.Error, "Duplicate Slug found", "Slug"));
                }
                if (!entity.Slug.MatchRegex(RegexHelper.Slug))
                {
                    validations.Add(new ATBusinessExceptionMessage(ATErrorLevel.Error, "Special characters except _ and - are not allowed in Slug.", "Slug"));
                }
            }
            return validations;
        }

    }

}
