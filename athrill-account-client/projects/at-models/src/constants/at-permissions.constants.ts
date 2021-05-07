export enum ATFeatures
{
    SiteSetting = 1,
    EmailSetting = 2,
    User = 3,
    UserTrackRecord = 4,
    RecordLogRecord = 5,
}

export class ATPermissions
{
    public static readonly FEATURE_SITE_SETTING : string = "FEATURE_SITE_SETTING";
    public static readonly RIGHT_VIEW_SITE_SETTING : string = "RIGHT_VIEW_SITE_SETTING";
    public static readonly RIGHT_EDIT_SITE_SETTING : string = "RIGHT_EDIT_SITE_SETTING";

    public static readonly FEATURE_EMAIL_SETTING : string = "FEATURE_EMAIL_SETTING";
    public static readonly RIGHT_VIEW_EMAIL_SETTING : string = "RIGHT_VIEW_EMAIL_SETTING";
    public static readonly RIGHT_ADD_EDIT_DELETE_EMAIL_SETTING : string = "RIGHT_ADD_EDIT_DELETE_EMAIL_SETTING";

    public static readonly FEATURE_USER : string = "FEATURE_USER";
    public static readonly RIGHT_VIEW_USER : string = "RIGHT_VIEW_USER";
    public static readonly RIGHT_ADD_EDIT_DELETE_USER : string = "RIGHT_ADD_EDIT_DELETE_USER";

    public static readonly FEATURE_USER_TRACK_RECORD : string = "FEATURE_USER_TRACK_RECORD";
    public static readonly RIGHT_VIEW_USER_TRACK_RECORD : string = "RIGHT_VIEW_USER_TRACK_RECORD";

    public static readonly FEATURE_RECORD_LOG_RECORD : string = "FEATURE_RECORD_LOG_RECORD";
    public static readonly RIGHT_VIEW_RECORD_LOG_RECORD : string = "RIGHT_VIEW_RECORD_LOG_RECORD";
}