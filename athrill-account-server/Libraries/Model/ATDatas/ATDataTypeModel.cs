using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.ATDatas
{
    public class ATDataTypeModel : BaseModel
    {
        public int ATDataTypeId { get; set; }
        public string NameKey { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


        public  ICollection<ATDataValueModel> ATDataValues { get; set; }

        public override int Id => ATDataTypeId;
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
