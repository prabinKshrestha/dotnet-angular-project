using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.ATDatas
{
    public class ATDataValueModel : BaseModel
    {
        public int ATDataValueId { get; set; }
        public int ATDataTypeId { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        #region link
        public  ATDataTypeModel ATDataType { get; set; }
        #endregion

        public override int Id => ATDataValueId;
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
