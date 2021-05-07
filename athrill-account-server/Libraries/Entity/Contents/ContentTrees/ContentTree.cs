using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Contents.ContentTrees
{
    public class ContentTree
    {
        public string Title { get; set; }
        public ContentTreeEntity Node { get; set; }
        public List<ContentTree> Children { get; set; }
    }
}
