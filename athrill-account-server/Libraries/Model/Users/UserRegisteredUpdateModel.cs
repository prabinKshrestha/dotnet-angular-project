using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Users
{
    public class UserRegisteredUpdateModel
    {
        public bool IsActive { get; set; }
        public int UserRoleId { get; set; }

    }

    public class UserRegisteredUpdateValidator : AbstractValidator<UserRegisteredUpdateModel>
    {
        public UserRegisteredUpdateValidator()
        {
            RuleFor(m => m.IsActive).NotNull();
            RuleFor(m => m.UserRoleId).NotNull();
        }
    }
}
