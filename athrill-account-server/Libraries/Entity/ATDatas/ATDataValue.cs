using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.System.ATDatas
{
    public class ATDataValue : BaseEntity , ISoftDeleteEntity
    {
        [RecordLogIgnore]
        public int ATDataValueId { get; set; }

        [RecordLogIgnore]
        public int ATDataTypeId { get; set; }

        public string DisplayName { get; set; }

        [RecordLogIgnore]
        public string Value { get; set; }

        [RecordLogIgnore]
        public string Description { get; set; }

        [RecordLogIgnore]
        public bool IsActive { get; set; }


        #region link

        [RecordLogIgnore]
        public virtual ATDataType ATDataType { get; set; }
        #endregion

        public override int Id => ATDataValueId;
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
