using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.SystemValues.ATEntities
{
    public class ATEntity : BaseEntity, ISoftDeleteEntity
    {
        public int EntityId { get;set; }
        public string NameKey { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsShownInRecordLogForSupportSite { get; set; }

        #region Override
        public override int Id => EntityId;

        #endregion
    }
}
