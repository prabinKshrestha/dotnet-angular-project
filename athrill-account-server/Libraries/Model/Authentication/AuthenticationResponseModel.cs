using AT.Common.Constants;
using AT.Model.Users;
using System;
using System.Collections.Generic;

namespace AT.Model.Authentication
{
    public class AuthenticationResponseModel
    {
        public string Token { get; set; }
        public DateTime TokenExpireTTL { get; set; }
        public bool? IsResetPassword { get; set; }
        public UserModel User { get; set; }
        public int UserRoleId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
