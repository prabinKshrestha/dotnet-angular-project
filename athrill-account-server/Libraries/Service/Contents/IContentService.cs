using AT.Entity.Contents;
using AT.Entity.Contents.ContentTrees;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Contents
{
    public interface IContentService: IBaseService<Content>, IQueryService<Content>
    {
        List<ContentTree> GetContentTrees();
        void UpdateContentTrees(List<ContentTree> contentTrees);
        List<ContentType> GetAllContentTypes();
    }
}
