using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity
{
    public abstract class BaseEntity
    {
        [RecordLogIgnore]
        public DateTime CreatedOn { get; set; }

        [RecordLogIgnore]
        public DateTime? UpdatedOn { get; set; }

        [RecordLogIgnore]
        public int CreatedById { get; set; }

        [RecordLogIgnore]
        public int UpdatedById { get; set; }

        [RecordLogIgnore]
        public DateTime? DeactivatedOn { get; set; }

        [RecordLogIgnore]
        public Guid InsertId { get; set; }

        [RecordLogIgnore]
        public Guid BatchId { get; set; }

        [RecordLogIgnore]
        public virtual int Id => throw new NotImplementedException("Id is not overridden");
    }
}
