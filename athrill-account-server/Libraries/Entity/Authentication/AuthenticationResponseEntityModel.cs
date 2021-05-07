using AT.Entity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Authentication
{
    public class AuthenticationResponseEntityModel
    {
        public string Token { get; set; }
        public bool? IsResetPassword { get; set; }
        public DateTime TokenExpireTTL { get; set; }
        public User User { get; set; }
        public int UserRoleId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
