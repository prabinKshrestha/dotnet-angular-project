using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Authentication
{
    public class UserRegistrationEntityModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public int GenderId { get; set; }
        public bool IsActive { get; set; }
        public string ImageName { get; set; }
        public int UserRoleId { get; set; }
        public string Username { get; set; }
    }
}
