using AT.Common.Enum;
using AT.Common.Helpers;
using System;

namespace AT.Model.Users
{
    public class UserTrackRecordModel
    {
        public int UserTrackRecordId { get; set; }
        public int UserId { get; set; }
        public int UserTrackTypeId { get; set; }
        public string IpAddress { get; set; }
        public string ClientName { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }

        #region Link
        public UserModel User { get; set; }
        #endregion

        public UserTrackTypes UserTrackType;

        public string UserTrackTypeDisplayName 
        { 
            get 
            {
                return UserTrackType.ToString();
            } 
        }
    }
}
