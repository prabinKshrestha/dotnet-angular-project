using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Users
{
    public class UserRoleModel
    {
        public int UserRoleId { get; set; }
        public string NameKey { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsShown { get; set; }

        #region Links
        public  ICollection<UserRoleLinkModel> UserRoleLinks { get; set; }
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
