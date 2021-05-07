
export enum UserTrackType {
    Login = 1,
    Logout,
    RegisterUser,
    Activate,
    Deactivate,
    ChangePassword,
    ResetPassword
}

export enum ATErrorLevel {
    Error = 1,
    Warning,
    Information
}

//system
export enum UserRoles {
    SuperAdmin = 1,
    Admin,
    Normal
}

export enum SortDirection {
    Ascending,
    Descending
}

export enum ContentTypes {
    None = 1,
    About = 2,
    Contact = 3,
    GeneralInformation = 4
}

export enum ForbidUserRoles {
    NoSuperAdmin = 1,
    NoAdmin,
    NoNormal
}

export enum ATResultType {
    Success = 1,
    Error,
    Warning,
    Information
}

export enum ATDataTypes {
    Gender = 1,
    ContentPlacement = 2
}

export enum ContentPlacements {
    None = 4,
    Top = 5,
    Buttom = 6
}

export enum RecordLogTypes {
    Added = 1,
    Modified = 2,
    Deleted = 3
}

export enum ATCustomDataType{
    Boolean,
    Integer,
    String,
    Date,
    Dropdown
}
