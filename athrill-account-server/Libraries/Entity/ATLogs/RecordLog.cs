using AT.Entity.SystemValues.ATEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.ATLogs
{
    public class RecordLog
    {
        public long RecordLogId { get; set; }
        public int RecordType { get; set; }
        public int EntityId { get; set; }
        public string Record { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid InsertId { get; set; }
        public Guid BatchId { get; set; }
        public string IPAddress { get; set; }
        public string ClientName { get; set; }

        #region Link
        public ATEntity Entity { get; set; }
        #endregion 
    }
}
