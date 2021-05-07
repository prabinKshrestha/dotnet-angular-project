using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Users
{
    public class UserLogin : BaseEntity, ISoftDeleteEntity
    {
        [RecordLogIgnore]
        public int UserLoginId { get; set; }

        [RecordLogIgnore]
        public int UserId { get; set; }
        public string Username { get; set; }

        [RecordLogIgnore]
        public string Password { get; set; }

        [RecordLogIgnore]
        public string SaltKey { get; set; }

        [RecordLogIgnore]
        public string OldPassword { get; set; }

        public bool? IsResetPassword { get; set; }

        #region Link 
        public virtual User User { get; set; }
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
