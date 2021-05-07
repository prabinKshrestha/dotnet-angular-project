using AT.Common.Api.Request;
using AT.Entity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Users
{
    public interface IUserService : IBaseService<User>, IQueryService<User>
    {
        void ChangeActiveStatus(int userId, bool activeStatus);
        IEnumerable<UserRole> GetUserRoles();
        IList<UserTrackRecord> GetUserTrackRecords(RequestBase request);
        int GetUserTrackRecordsCount(RequestBase request);

        void UpdateRegisteredUser(int userId, UserRegisteredUpdateEntityModel model);
    }
}
