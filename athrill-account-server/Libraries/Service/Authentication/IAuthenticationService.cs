using AT.Entity.Authentication;
using AT.Entity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResponseEntityModel Authenticate(AuthenticationRequestEntityModel loginEntityModel);
        void SignOut();
        User RegisterUser(UserRegistrationEntityModel userRegistrationEntityModel);
        void ResetPassword(ResetPasswordEnityModel resetPasswordEnityModel);
        void ChangePassword(ChangePasswordEntityModel changePasswordEntityModel);
    }
}
