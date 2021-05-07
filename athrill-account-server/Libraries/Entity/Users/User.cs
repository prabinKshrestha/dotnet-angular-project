using AT.Common.Attributes;
using AT.Entity.System.ATDatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Users
{
    public class User : BaseEntity, ISoftDeleteEntity, IImageEntity
    {
        [RecordLogIgnore]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ImageName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }

        [RecordLogIgnore]
        public int GenderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        #region Links 

        [RecordLogIgnore]
        public virtual UserLogin UserLogin { get; set; }

        [RecordLogIgnore]
        public virtual UserRoleLink UserRoleLink { get; set; }

        [RecordLogIgnore]
        public virtual ICollection<UserTrackRecord> UserTrackRecords { get; set; }
        public virtual ATDataValue Gender { get; set; }
        #endregion

        #region override
        public override int Id => UserId;
        public override string ToString()
        {
            return string.Concat(FirstName, MiddleName, LastName);
        }
        #endregion
    }
}
