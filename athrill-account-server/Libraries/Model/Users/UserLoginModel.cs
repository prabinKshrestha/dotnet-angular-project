using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Users
{
    public class UserLoginModel : BaseModel
    {
        public int UserLoginId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool? IsResetPassword { get; set; }

        #region Link 
        public  UserModel User { get; set; }
        #endregion

        #region override
        public override int Id => UserLoginId;
        public override string ToString()
        {
            return Username;
        }
        #endregion
    }
}
