using AT.Entity.ATLogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.ATLogs
{
    public interface IRecordLogService : IBaseService<RecordLog>, IQueryService<RecordLog>
    {
        RecordLog GetLastRecordLog(int id);
    }
}
