using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Data.Interface;
using AT.Entity.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service.Users.Rules
{
    public class UserServiceRule : IUserServiceRule
    {
        private readonly IRepository<User> _userRepository; 
        private readonly IRepository<UserRoleLink> _userRoleLinkRepository;
        private readonly IRepository<UserRole> _userRoleRepository;

        public UserServiceRule(IRepository<User> userRepository
            , IRepository<UserRoleLink> userRoleLinkRepository
            , IRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleLinkRepository = userRoleLinkRepository;
            _userRoleRepository = userRoleRepository;
        }      

        public void CheckAddRule(User entity)
        {
            throw new NotImplementedException();
        }

        public void CheckDeleteRule(User entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            if (_userRoleLinkRepository.TableNotTracked.FirstOrDefault(x => x.UserId == entity.UserId).UserRoleId == (int)UserRoles.Admin 
                && _userRoleLinkRepository.TableNotTracked.Count(x => x.UserRoleId == (int)UserRoles.Admin && x.UserId != entity.UserId) == 0)
            {
                validations.Add(new ATBusinessExceptionMessage("System need at least one Admin User.", "User"));
            }
            if (validations.Any())
            {
                throw new ATBusinessException("User Business Rule Violations", validations);
            }
        }

        public void CheckUpdateRule(User entity)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            if (_userRepository.TableNotTracked.Any(x => x.Email == entity.Email && x.UserId != entity.UserId))
            {
                throw new ATBusinessException("Email validation message", new List<ATBusinessExceptionMessage> { new ATBusinessExceptionMessage(ATErrorLevel.Error, "Email already exist in the system. Please try another email.", "Email") });
            }
        }

        public void CheckUserRegisteredUpdateRule(int userId, UserRegisteredUpdateEntityModel model)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();
            if (!_userRepository.VerifyByReference(userId))
            {
                validations.Add(new ATBusinessExceptionMessage("Not valid User.", "User"));
            }
            if (!_userRoleRepository.VerifyByReference(model.UserRoleId))
            {
                validations.Add(new ATBusinessExceptionMessage("Not valid User Role.", "User Role"));
            }
            if (validations.Any())
            {
                throw new ATBusinessException("User Update Busineess Rule Violation", validations);
            }
        }

        public void CheckChangeActiveStatusRule(User entity, bool activeStatus)
        {
            List<ATBusinessExceptionMessage> validations = new List<ATBusinessExceptionMessage>();

            if (_userRoleLinkRepository.TableNotTracked.FirstOrDefault(x => x.UserId == entity.UserId).UserRoleId == (int)UserRoles.Admin
                && _userRepository.TableNotTracked.Include(x => x.UserRoleLink).Count(x => x.IsActive && x.UserRoleLink.UserRoleId == (int)UserRoles.Admin && x.UserId != entity.UserId) == 0)
            {
                validations.Add(new ATBusinessExceptionMessage("System needs at least one active user.","Active Status"));
            }
            if (validations.Any())
            {
                throw new ATBusinessException("User Busineess Rule Violation", validations);
            }
        }
    }
}

