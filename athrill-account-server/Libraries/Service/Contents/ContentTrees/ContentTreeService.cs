using AT.Entity.Contents.ContentTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Contents.ContentTrees
{
    public class ContentTreeService : IContentTreeService
    {
        public List<ContentTreeEntity> ContentTreeEntities { get; set; }
        public ContentTreeService()
        {
            ContentTreeEntities = new List<ContentTreeEntity>();
        }
        public List<ContentTree> GenerateContentTree(List<ContentTreeEntity> nodes)
        {
            List<ContentTree> mainTreeRepositories = new List<ContentTree>();
            foreach (ContentTreeEntity root in nodes.Where(x => x.ParentId == null).ToList())
            {
                ContentTree treeRepository = new ContentTree
                {
                    Node = root,
                    Title = root.Name
                };
                if (!nodes.Any(x => x.ParentId == root.ContentId))
                {
                    treeRepository.Children = null;
                    mainTreeRepositories.Add(treeRepository);
                }
                else
                {
                    mainTreeRepositories.Add(GenerateTreeLeaves(treeRepository, nodes, root));
                }
            }
            return mainTreeRepositories;
        }
        private ContentTree GenerateTreeLeaves(ContentTree parentTreeRepository, List<ContentTreeEntity> nodes, ContentTreeEntity root)
        {
            foreach (ContentTreeEntity node in nodes.Where(x => x.ParentId == root.ContentId).ToList())
            {
                ContentTree childTreeRepository = new ContentTree
                {
                    Node = node,
                    Title = node.Name
                };
                if (nodes.Any(x => x.ParentId == node.ContentId))
                {
                    GenerateTreeLeaves(childTreeRepository, nodes, node);
                }
                else
                {
                    childTreeRepository.Children = null;
                }
                if (parentTreeRepository.Children != null)
                {
                    parentTreeRepository.Children.Add(childTreeRepository);
                }
                else
                {
                    parentTreeRepository.Children = new List<ContentTree>
                    {
                        childTreeRepository
                    };
                }
            }
            return parentTreeRepository;
        }
        public void SetUpdatedContentTree(List<ContentTree> parentContentTreeEntity, int? parentId = null)
        {
            foreach (ContentTree parentTreeRepository in parentContentTreeEntity)
            {
                parentTreeRepository.Node.ParentId = parentId;
                this.ContentTreeEntities.Add(parentTreeRepository.Node);
                if (parentTreeRepository.Children != null)
                {
                    SetUpdatedContentTree(parentTreeRepository.Children, parentTreeRepository.Node.ContentId);
                }
            }
        }
    }
}
