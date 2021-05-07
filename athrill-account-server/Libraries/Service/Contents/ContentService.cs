using AT.Common.Api.Infrastructure;
using AT.Common.Api.Request;
using AT.Common.Helpers;
using AT.Data.Interface;
using AT.Data.Request;
using AT.Entity.Contents;
using AT.Entity.Contents.ContentTrees;
using AT.Service.Contents.ContentTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Contents
{
    public class ContentService : QueryService<Content>,IContentService
    {
        private readonly IRepository<Content> _contentRepository;
        private readonly IRepository<ContentType> _contentTypeRepository;
        private readonly IContentTreeService _contentTreeService;
        private readonly IContentServiceRule _contentServiceRule;
        private readonly IUnitOfWork _unitOfWork;
        public ContentService(
            IRepository<Content> contentRepository,
            IRepository<ContentType> contentTypeRepository,
            IContentTreeService contentTreeService,
            IContentServiceRule contentServiceRule,
            IUnitOfWork unitOfWork
        )
        {
            _contentRepository = contentRepository;
            _contentTypeRepository = contentTypeRepository;
            _contentTreeService = contentTreeService;
            _contentServiceRule = contentServiceRule;
            _unitOfWork = unitOfWork;
        }

        public Content Get(int id)
        {
            return _contentRepository.Get(id);
        }

        public IEnumerable<Content> GetAll()
        {
            return _contentRepository.GetAll();
        }
        public override Content GetById(int id, GetByIdRequestBase request = null)
        {
            return GetQueryForId(request).FinalizeQueryById(x => x.ContentId == id, id);
        }

        public List<ContentType> GetAllContentTypes()
        {
            return _contentTypeRepository.GetAll().ToList();
        }


        #region Modify Methods

        public void Add(Content entity)
        {
            entity.Position = (_contentRepository.TableNotTracked.Select(p => p.Position).Cast<int?>().Max() ?? 0) + 1;
            entity.ParentId = entity.ParentId == 0 ? null : entity.ParentId;
            _contentServiceRule.CheckAddRule(entity);
            if (string.IsNullOrEmpty(entity.Slug))
            {
                entity.Slug = GetSlug(entity);
            }
            _contentRepository.Add(entity);
            _unitOfWork.Commit();
        }
        public void Update(Content entity)
        {
            entity.ParentId = entity.ParentId == 0 ? null : entity.ParentId;
            _contentServiceRule.CheckUpdateRule(entity);
            if (string.IsNullOrEmpty(entity.Slug))
            {
                entity.Slug = GetSlug(entity);
            }
            _contentRepository.Update(entity);
            _unitOfWork.Commit();
        }
        public void Delete(Content entity)
        {
            _contentServiceRule.CheckDeleteRule(entity);
            entity.Position = 0;
            _contentRepository.Delete(entity);
            _unitOfWork.Commit();
        }
        #endregion

        #region Content Trees
        public List<ContentTree> GetContentTrees()
        {
            // content placement where predicate is necessary
            List<ContentTreeEntity> orignialContents = _contentRepository.TableNotTracked
                                                                            .OrderBy(x => x.Position)
                                                                            .Select(x => new ContentTreeEntity
                                                                            {
                                                                                ContentId = x.ContentId,
                                                                                ParentId = x.ParentId,
                                                                                PlacementId = x.PlacementId,
                                                                                Position = x.Position,
                                                                                Name = x.Name,
                                                                                ExternalUrl = x.ExternalUrl,
                                                                                Slug = x.Slug,
                                                                                IsPublished = x.IsPublished
                                                                            })
                                                                            .ToList();
            return _contentTreeService.GenerateContentTree(orignialContents);
        }

        public void UpdateContentTrees(List<ContentTree> contentTrees)
        {
            List<Content> allContents = GetAll().ToList();
            _contentTreeService.SetUpdatedContentTree(contentTrees);
            int i = 1;
            foreach (ContentTreeEntity contentTreeEntity in _contentTreeService.ContentTreeEntities)
            {
                Content content = allContents.FirstOrDefault(x => x.ContentId == contentTreeEntity.ContentId);
                content.ParentId = contentTreeEntity.ParentId;
                content.Position = i;
                _contentRepository.Update(content);
                i++;
            }
            _unitOfWork.Commit();
        }
        #endregion

        #region Private Methods

        public override void SetRepository()
        {
            QueryRepository = _contentRepository;
        }

        public override void SetDefaultOrderByItem()
        {
            DefaultOrderByItem = new OrderByItem("CreatedOn", "desc");
        }
        private string GetSlug(Content entity)
        {
            string slug = ServiceHelper.MakeSlug(entity.Name);
            while (_contentRepository.Table.Any(x => x.Slug == slug && x.ContentId != entity.ContentId))
            {
                Random random = new Random();
                slug += random.Next(1, 9).ToString();
            }
            return slug;
        }
        #endregion
    }
}
