using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model
{
    public abstract class BaseModel
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
        public abstract int Id { get; }
    }
    public abstract class BaseAddModel
    {
    }
    public abstract class BaseUpdateModel
    {
    }
}
