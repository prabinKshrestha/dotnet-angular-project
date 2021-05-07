using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Common.Helpers;
using AT.Data.Interface;
using AT.Entity.Authentication;
using AT.Entity.System.ATDatas;
using AT.Entity.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Authentication.Rules
{
    public class AuthenticationServiceRule : IAuthenticationServiceRule
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserLogin> _userLoginRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<ATDataValue> _atDataValueRepository;
        private readonly IWorkContext _workContext;
        public AuthenticationServiceRule(IRepository<User> userRepository
            , IRepository<UserLogin> userLoginRepository
            , IRepository<UserRole> userRoleRepository
            , IRepository<ATDataValue> atDataValueRepository
            , IWorkContext workContext)
        {
            _userRepository = userRepository;
            _userLoginRepository = userLoginRepository;
            _userRoleRepository = userRoleRepository;
            _atDataValueRepository = atDataValueRepository;
            _workContext = workContext;
        }

        public void CheckChangePasswordRule(ChangePasswordEntityModel changePasswordEntityModel)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();

            UserLogin userLogin = _userLoginRepository.TableNotTracked.FirstOrDefault(x => x.UserLoginId == _workContext.UserLoginId);

            if(userLogin == null) 
            {
                throw new ATReferenceException(_workContext.UserLoginId, nameof(UserLogin));
            }
            if (SecurityHelper.GetHashPassword(changePasswordEntityModel.OldPassword, userLogin.SaltKey) != userLogin.Password)
            {
                validations.Add(new ATBusinessExceptionMessage("Old Password is not correct.", "Old Password"));
            }
            validations.AddRange(CheckPassword(changePasswordEntityModel.NewPassword));
            if (validations.Any())
            {
                throw new ATBusinessException("Password Change Violation.", validations);
            }
        }

        public void CheckResetPasswordRule(ResetPasswordEnityModel resetPasswordEnityModel)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            bool notExist = false;
            if (!_userRepository.TableNotTracked.Any(x => x.Email == resetPasswordEnityModel.Email))
            {
                notExist = true;
                validations.Add(new ATBusinessExceptionMessage("Email doesnot exist in our system.", "Email"));
            }
            if (!_userLoginRepository.TableNotTracked.Any(x => x.Username == resetPasswordEnityModel.Username))
            {
                notExist = true;
                validations.Add(new ATBusinessExceptionMessage("Username doesnot exist in our system.", "Username"));
            }
            if (!notExist && !_userLoginRepository.TableNotTracked.Include(x => x.User).Any(x => x.Username == resetPasswordEnityModel.Username && x.User.Email == resetPasswordEnityModel.Email))
            {
                validations.Add(new ATBusinessExceptionMessage("Email and Username are not related.", "Username and Email"));
            }
            if (validations.Any())
            {
                throw new ATBusinessException("Reset Password Business Validation" , validations);
            }
        }

        public void CheckUserRegistrationRule(UserRegistrationEntityModel userRegistrationEntityModel)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            if (_userRepository.TableNotTracked.ToList().Any(x => string.Equals(x.Email, userRegistrationEntityModel.Email, StringComparison.OrdinalIgnoreCase)))
            {
                validations.Add(new ATBusinessExceptionMessage("Email cannot be duplicated.", "Email"));
            }
            if (_userLoginRepository.TableNotTracked.ToList().Any(x => string.Equals(x.Username, userRegistrationEntityModel.Username, StringComparison.OrdinalIgnoreCase)))
            {
                validations.Add(new ATBusinessExceptionMessage("User Name cannot be duplicated.", "User Name"));
            }
            if (string.IsNullOrWhiteSpace(userRegistrationEntityModel.ImageName))
            {
                validations.Add(new ATBusinessExceptionMessage("Image Name is required.", "Image Name"));
            }
            if (!_atDataValueRepository.VerifyByReferenceForATDataValues(userRegistrationEntityModel.GenderId, ATDataTypes.Gender))
            {
                validations.Add(new ATBusinessExceptionMessage("Not valid Gender.", "Gender"));
            }
            if (!_userRoleRepository.VerifyByReference((int)userRegistrationEntityModel.UserRoleId))
            {
                validations.Add(new ATBusinessExceptionMessage("User Role cannot be found."));
            }
            if (validations.Any())
            {
                throw new ATBusinessException("User Registration Business Validation", validations);
            }
        }

        #region Private

        private IEnumerable<ATBusinessExceptionMessage> CheckPassword(string newPassword)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            if (!newPassword.MatchRegex(RegexHelper.Password))
            {
                validations.Add(new ATBusinessExceptionMessage("Password should have atleast 8 characters, one small letter, one capital letter, one number and one special character.", "Password"));
            }
            return validations;
        }

        #endregion
    }
}
