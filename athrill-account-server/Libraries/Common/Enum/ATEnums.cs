using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AT.Common.Enum
{
    public enum RecordLogTypes
    {
        Added = 1,
        Modified = 2,
        Deleted = 3
    }
    public enum UserTrackTypes
    {
        Login = 1,
        Logout,
        RegisterUser,
        Activate,
        Deactivate,
        ChangePassword,
        ResetPassword
    }
    public enum ATErrorLevel
    {
        Error = 1,
        Warning,
        Information
    }
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    //system
    public enum UserRoles
    {
        [Display(Name = "Super Admin")]
        SuperAdmin = 1,

        [Display(Name = "Admin User")]
        Admin,

        [Display(Name = "Normal User")]
        Normal
    }

    public enum ForbidUserRoles
    {
        NoSuperAdmin = 1,
        NoAdmin,
        NoNormal
    }
    public enum ATSettings
    {
        SiteSettingId = 1
    }

    public enum ContentTypes
    { 
        None = 1,
        About = 2,
        Contact = 3,
        GeneralInformation = 4
    }

    public enum ATDataTypes
    {
        Gender = 1,
        ContentPlacement = 2
    }

    public enum ContentPlacements
    {
        None = 4,
        Top = 5,
        Buttom = 6
    }


    public enum ATExceptionTypes
    {
        [Display(Name = "Unkown")]
        Unkown,

        [Display(Name = "Null Reference Execption")]
        NullReferenceExecption,

        [Display(Name = "Application Exception")]
        ApplicationException,

        [Display(Name = "Sql Exception")]
        SqlException,

        [Display(Name = "Database Exception")]
        DbUpdateException,

        [Display(Name = "Parse Exception")]
        ODataParseException,

        [Display(Name = "User Role Forbidden Exception")]
        ATUserRoleForbiddenException,

        [Display(Name = "Authentication Exception")]
        ATAuthenticationException,

        [Display(Name = "Reference Exception")]
        ATReferenceException,

        [Display(Name = "Business Exception")]
        ATBusinessException
    }

    public enum ClientApplication
    {

        [Display(Name = "Support Site Application")]
        SupportSite,

        [Display(Name = "Client Application")]
        Client,

        [Display(Name = "Mobile Application")]
        Mobile
    }

}
