using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Users
{
    /// <summary>
    ///  This is System Value.
    ///  This is User Role
    /// </summary>
    public class UserRole
    {
        [RecordLogIgnore]
        public int UserRoleId { get; set; }

        [RecordLogIgnore]
        public string NameKey { get; set; }
        public string DisplayName { get; set; }

        [RecordLogIgnore]
        public string Description { get; set; }

        [RecordLogIgnore]
        public bool IsShown { get; set; }

        #region Links

        [RecordLogIgnore]
        public virtual ICollection<UserRoleLink> UserRoleLinks { get; set; }
        #endregion

        #region override
        public int Id => UserRoleId;
        public override string ToString()
        {
            return DisplayName;
        }
        #endregion
    }
}
