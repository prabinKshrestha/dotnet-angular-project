using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Contents
{
    public class ContentType : BaseEntity
    {
        [RecordLogIgnore]
        public int ContentTypeId { get; set; }

        [RecordLogIgnore]
        public string NameKey { get; set; }
        public string DisplayName { get; set; }

        [RecordLogIgnore]
        public string Description { get; set; }

        [RecordLogIgnore]
        public bool IsActive { get; set; }

        [RecordLogIgnore]
        public virtual ICollection<Content> Contents { get; set; }


        #region overrride
        public  override int Id => ContentTypeId;
        #endregion

    }
}
