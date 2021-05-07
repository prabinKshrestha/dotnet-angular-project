using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Users
{
    public class UserRoleLink : BaseEntity, ISoftDeleteEntity
    {

        [RecordLogIgnore]
        public int UserRoleLinkId { get; set; }

        [RecordLogIgnore]
        public int UserId { get; set; }

        [RecordLogIgnore]
        public int UserRoleId { get; set; }

        #region links
        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
        #endregion

        #region override
        public override int Id => UserRoleLinkId;
        #endregion
    }
}
