using AT.Entity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Users.Rules
{
    public interface IUserServiceRule:IBaseServiceRule<User>
    {
        void CheckChangeActiveStatusRule(User entity, bool activeStatus);
        void CheckUserRegisteredUpdateRule(int userId, UserRegisteredUpdateEntityModel model);
    }
}
