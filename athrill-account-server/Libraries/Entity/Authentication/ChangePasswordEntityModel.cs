using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Authentication
{
    public class ChangePasswordEntityModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
