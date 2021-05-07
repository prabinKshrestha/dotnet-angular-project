using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Users
{
    public class UserRoleLinkModel : BaseModel
    {
        public int UserRoleLinkId { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }

        #region links
        public  UserModel User { get; set; }
        public  UserRoleModel UserRole { get; set; }
        #endregion

        #region override
        public override int Id => UserRoleLinkId;
        #endregion
    }
}
