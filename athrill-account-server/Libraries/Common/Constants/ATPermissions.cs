using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Constants
{
    public enum ATFeatures
    {
        SiteSetting = 1,
        EmailSetting = 2,
        User = 3,
        UserTrackRecord = 4,
        RecordLogRecord = 5,
    }

    // This will be necessary if we are going to move rights to the table
    public enum ATUserRights
    {
        ViewSiteSetting = 1,
        EditSiteSetting,
        ViewEmailSetting,
        AddEditDeleteEmailSetting,
        ViewUser,
        AddEditDeleteUser,
        ViewUserTrackRecord,
        ViewRecordLogRecord
    }
    public class ATPermissions
    {
        public const string FEATURE_SITE_SETTING = "FEATURE_SITE_SETTING";
        public const string RIGHT_VIEW_SITE_SETTING = "RIGHT_VIEW_SITE_SETTING";
        public const string RIGHT_EDIT_SITE_SETTING = "RIGHT_EDIT_SITE_SETTING";

        public const string FEATURE_EMAIL_SETTING = "FEATURE_EMAIL_SETTING";
        public const string RIGHT_VIEW_EMAIL_SETTING = "RIGHT_VIEW_EMAIL_SETTING";
        public const string RIGHT_ADD_EDIT_DELETE_EMAIL_SETTING = "RIGHT_ADD_EDIT_DELETE_EMAIL_SETTING";

        public const string FEATURE_USER = "FEATURE_USER";
        public const string RIGHT_VIEW_USER = "RIGHT_VIEW_USER";
        public const string RIGHT_ADD_EDIT_DELETE_USER = "RIGHT_ADD_EDIT_DELETE_USER";

        public const string FEATURE_USER_TRACK_RECORD = "FEATURE_USER_TRACK_RECORD";
        public const string RIGHT_VIEW_USER_TRACK_RECORD = "RIGHT_VIEW_USER_TRACK_RECORD";

        public const string FEATURE_RECORD_LOG_RECORD = "FEATURE_RECORD_LOG_RECORD";
        public const string RIGHT_VIEW_RECORD_LOG_RECORD = "RIGHT_VIEW_RECORD_LOG_RECORD";

        public static List<string> GetUserRightsByRole(UserRoles userRole)
        {
            List<string> retVal = new List<string>();
            switch (userRole)
            {
                case UserRoles.SuperAdmin:
                    retVal.AddRange(new List<string>()
                    {
                        RIGHT_VIEW_SITE_SETTING,
                        RIGHT_EDIT_SITE_SETTING,
                        RIGHT_VIEW_EMAIL_SETTING,
                        RIGHT_ADD_EDIT_DELETE_EMAIL_SETTING,
                        RIGHT_VIEW_USER,
                        RIGHT_ADD_EDIT_DELETE_USER,
                        RIGHT_VIEW_USER_TRACK_RECORD,
                        RIGHT_VIEW_RECORD_LOG_RECORD,
                    });
                    break;
                case UserRoles.Admin:
                    retVal.AddRange(new List<string>()
                    {
                        RIGHT_VIEW_SITE_SETTING,
                        RIGHT_EDIT_SITE_SETTING,
                        RIGHT_VIEW_EMAIL_SETTING,
                        RIGHT_ADD_EDIT_DELETE_EMAIL_SETTING,
                        RIGHT_VIEW_USER,
                        RIGHT_ADD_EDIT_DELETE_USER
                    });
                    break;
                case UserRoles.Normal:
                    retVal.AddRange(new List<string>()
                    {
                        RIGHT_VIEW_SITE_SETTING,
                        RIGHT_VIEW_EMAIL_SETTING,
                        RIGHT_VIEW_USER
                    });
                    break;
            }
            return retVal;
        }
    }
}
