using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Contents
{
    public class ContentTypeModel : BaseModel
    {
        public int ContentTypeId { get; set; }
        public string NameKey { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public override int Id => ContentTypeId;

        #region Link
        public  ICollection<ContentModel> Contents { get; set; }

        #endregion

    }
}
