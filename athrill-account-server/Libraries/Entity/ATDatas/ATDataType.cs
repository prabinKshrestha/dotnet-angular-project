using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.System.ATDatas
{
    public class ATDataType : BaseEntity, ISoftDeleteEntity
    {
        public int ATDataTypeId { get; set; }
        public string NameKey { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


        public virtual ICollection<ATDataValue> ATDataValues { get; set; }

        public override int Id => ATDataTypeId;
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
