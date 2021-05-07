using AT.Common.Enum;
using System;

namespace AT.Entity.Users
{
    public class UserTrackRecord
    {
        public int UserTrackRecordId { get; set; }
        public int UserId { get; set; }
        public int UserTrackTypeId { get; set; }
        public string IpAddress { get; set; }
        public string ClientName { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }

        #region Link
        public User User { get; set; }
        #endregion

        public UserTrackTypes UserTrackType => (UserTrackTypes)UserTrackTypeId;
    }
}
