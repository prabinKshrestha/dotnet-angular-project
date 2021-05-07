using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Authentication
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(m => m.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(m => m.Username).NotEmpty().NotNull();
        }
    }
}
