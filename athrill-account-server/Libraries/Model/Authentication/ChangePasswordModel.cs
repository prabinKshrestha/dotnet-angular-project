using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Authentication
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewConfirmationPassword { get; set; }
    }
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidator()
        {
            RuleFor(m => m.OldPassword).NotEmpty().NotNull();
            RuleFor(m => m.NewPassword).NotEmpty().NotNull().NotEqual(customer => customer.OldPassword).WithMessage("Old password and New password cannot be same.");
            RuleFor(m => m.NewConfirmationPassword).Equal(customer => customer.NewPassword).WithMessage("Passwords do not match.");
        }
    }
}
