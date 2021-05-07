using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Authentication
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(m => m.Username).NotEmpty().NotNull().Length(1, 50); 
            RuleFor(m => m.Password).NotEmpty().NotNull();
        }
    }
}
