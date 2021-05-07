using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Contents.ContentTrees
{
    public class ContentTreeEntity
    {
        public int ContentId { get; set; }
        public int? ParentId { get; set; }
        public int PlacementId { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ExternalUrl { get; set; }
        public bool IsPublished { get; set; }
    }
}
