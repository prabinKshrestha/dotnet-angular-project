using AT.Entity.System.ATDatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.ATDatas
{
    public interface IATDatasService : IBaseService<ATDataValue>
    {
        List<ATDataValue> GetATDataValuesByType(int atDataTypeId); 
    }
}
