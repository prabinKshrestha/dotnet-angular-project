using AT.Common.Attributes;
using AT.Entity.System.ATDatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Contents
{
    public class Content : BaseEntity, IImageEntity, ISoftDeleteEntity
    {
        [RecordLogIgnore]
        public int ContentId { get; set; }

        [RecordLogIgnore]
        public int ContentTypeId { get; set; }

        [RecordLogIgnore]
        public int? ParentId { get; set; }

        [RecordLogIgnore]
        public int PlacementId { get; set; }
        public int Position { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string ImageName { get; set; }
        public string ImageAltName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ExternalUrl { get; set; }
        public string FontAwesomeIcon { get; set; }
        public bool IsPublished { get; set; }

        public virtual ContentType ContentType { get; set; }
        public virtual Content Parent { get; set; }
        public virtual ATDataValue Placement { get; set; }

        [RecordLogIgnore]
        public virtual ICollection<Content> Childrens { get; set; }



        #region overrride
        public override int Id => ContentId;

        #endregion
    }
}
