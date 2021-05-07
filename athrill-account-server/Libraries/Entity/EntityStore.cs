using AT.Entity.ATLogs;
using AT.Entity.Contents;
using AT.Entity.Settings.EmailSettings;
using AT.Entity.Settings.SiteSettings;
using AT.Entity.System.ATDatas;
using AT.Entity.Users;
using System.Collections.Generic;
using AT.Entity.SystemValues.ATEntities;
using AT.Entity.Basics.Teams;

namespace AT.Entity
{
    public static class EntityStore
    {
        public static Dictionary<string, ATEntities> EntityNameEnumPair = new Dictionary<string, ATEntities>()
        {
            { typeof(ATEntity).Name                                 ,   ATEntities.ATEntity },
            { typeof(User).Name                                     ,   ATEntities.User },
            { typeof(UserLogin).Name                                ,   ATEntities.UserLogin },
            { typeof(UserRole).Name                                 ,   ATEntities.UserRole },
            { typeof(UserRoleLink).Name                             ,   ATEntities.UserRoleLink },
            { typeof(UserTrackRecord).Name                          ,   ATEntities.UserTrackRecord },
            { typeof(ATDataType).Name                               ,   ATEntities.ATDataType },
            { typeof(ATDataValue).Name                              ,   ATEntities.ATDataValue },
            { typeof(RecordLog).Name                                ,   ATEntities.RecordLog },
            { typeof(EmailSetting).Name                             ,   ATEntities.EmailSetting },
            { typeof(SiteSetting).Name                              ,   ATEntities.SiteSetting },
            { typeof(ContentType).Name                              ,   ATEntities.ContentType },
            { typeof(Content).Name                                  ,   ATEntities.Content },
            { typeof(TeamCategory).Name                             ,   ATEntities.TeamCategory },
            { typeof(TeamMember).Name                               ,   ATEntities.TeamMember },
            { typeof(TeamCategoryMemberLink).Name                   ,   ATEntities.TeamCategoryMemberLink },
        };
    }
    
    // never change number or order. If any entity is added, then add at last
    public enum ATEntities
    {
        ATEntity = 1,
        User = 2,
        UserLogin = 3,
        UserRole = 4,
        UserRoleLink = 5,
        UserTrackRecord = 6,
        ATDataType = 7,
        ATDataValue = 8,
        RecordLog = 9,
        EmailSetting = 10,
        SiteSetting = 11,
        ContentType = 12,
        Content = 13,
        TeamCategory = 14,
        TeamMember = 15,
        TeamCategoryMemberLink = 16,
    }
}
