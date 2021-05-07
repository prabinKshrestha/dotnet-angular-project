using AT.Entity.Contents.ContentTrees;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Contents.ContentTrees
{
    public interface IContentTreeService
    {
        List<ContentTreeEntity> ContentTreeEntities { get; set; }
        List<ContentTree> GenerateContentTree(List<ContentTreeEntity> nodes);
        void SetUpdatedContentTree(List<ContentTree> parentContentTreeEntity, int? parentId = null);
    }
}
