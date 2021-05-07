using AT.Model.SystemValues.ATEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.ATLogs
{
    public class RecordLogModel
    {
        public long RecordLogId { get; set; }
        public int RecordType { get; set; }
        public int EntityId { get; set; }
        public string Record { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string IPAddress { get; set; }
        public string ClientName { get; set; }
        public long Id => RecordLogId;
        public ATEntityModel Entity { get; set; }

        [JsonIgnore]
        public Guid InsertId { get; set; }
        [JsonIgnore]
        public Guid BatchId { get; set; }
    }
}
