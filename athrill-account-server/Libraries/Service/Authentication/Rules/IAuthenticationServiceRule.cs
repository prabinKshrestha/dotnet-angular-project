using AT.Entity.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Authentication.Rules
{
    public interface IAuthenticationServiceRule
    {
        void CheckResetPasswordRule(ResetPasswordEnityModel resetPasswordEnityModel);
        void CheckChangePasswordRule(ChangePasswordEntityModel changePasswordEntityModel);
        void CheckUserRegistrationRule(UserRegistrationEntityModel userRegistrationEntityModel);
    }
}
