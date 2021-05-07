using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Users
{
    public class UserUpdateModel : BaseUpdateModel, IImageUpdateModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int GenderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public IFormFile ImageFile { get; set; }
        public string FilePath => "images/user/";
        public bool IsImageRequired => true;
        public bool IsImageChanged { get; set; }
    }

    public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateModelValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().NotNull().Length(1, 50);
            RuleFor(m => m.MiddleName).Length(1, 50);
            RuleFor(m => m.LastName).NotEmpty().NotNull().Length(1, 50);
            RuleFor(m => m.Email).NotEmpty().NotNull().Length(1, 200).EmailAddress();
            RuleFor(m => m.PhoneNumber).NotEmpty().NotNull();
            RuleFor(m => m.Address).NotEmpty().NotNull();
            RuleFor(m => m.DOB).LessThan(DateTime.Today);
            RuleFor(m => m.GenderId).NotNull();
            RuleFor(m => m.IsActive).NotNull();
        }
    }
}
