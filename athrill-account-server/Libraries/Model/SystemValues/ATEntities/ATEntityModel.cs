using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.SystemValues.ATEntities
{
    public class ATEntityModel : BaseModel
    {
        public int EntityId { get; set; }
        [JsonIgnore]
        public string NameKey { get; set; }
        public string DisplayName { get; set; }
        [JsonIgnore]
        public string Description { get; set; }
        [JsonIgnore]
        public bool IsShownInRecordLogForSupportSite { get; set; }

        #region Override
        public override int Id => EntityId;
        #endregion
    }
}
