using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Authentication
{
    public class AuthenticationRequestEntityModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
        public string ClientName { get; set; }
    }
}
