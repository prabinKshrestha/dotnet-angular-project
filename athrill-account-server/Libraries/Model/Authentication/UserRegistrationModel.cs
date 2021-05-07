using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Authentication
{
    public class UserRegistrationModel : IImageAddModel
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
        public int UserRoleId { get; set; }
        public string Username { get; set; }
        public string FilePath => "images/user/";
        public IFormFile ImageFile { get; set; }
        public bool IsImageRequired => true;
    }

    public class UserRegistrationValidator : AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().NotNull().Length(1, 50);
            RuleFor(m => m.MiddleName).Length(1, 50);
            RuleFor(m => m.LastName).NotEmpty().NotNull().Length(1, 50);
            RuleFor(m => m.Email).NotEmpty().NotNull().Length(1, 200).EmailAddress();
            RuleFor(m => m.PhoneNumber).NotEmpty().NotNull();
            RuleFor(m => m.Address).NotEmpty().NotNull();
            RuleFor(m => m.DOB).NotNull().LessThan(DateTime.Today);
            RuleFor(m => m.GenderId).NotNull();
            RuleFor(m => m.IsActive).NotNull();
            RuleFor(m => m.UserRoleId).NotNull();
            RuleFor(m => m.Username).NotEmpty().NotNull().Length(1, 50);
        }
    }
}
