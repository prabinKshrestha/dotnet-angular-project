using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Authentication
{
    public class ResetPasswordEnityModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string IpAddress { get; set; }
        public string ClientName { get; set; }
    }
}
