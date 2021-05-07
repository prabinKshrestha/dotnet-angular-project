using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Exceptions;
using AT.Model.Exceptions;
using AT.Model.Users;
using AT.Entity.Users;
using AT.Entity.Authentication;
using AT.Model.Authentication;
using AT.Entity.System.ATDatas;
using AT.Model.ATDatas;
using AT.Entity.Settings.EmailSettings;
using AT.Model.Settings.EmailSettings;
using AT.Entity.Settings.SiteSettings;
using AT.Model.Settings.SiteSettings;
using AT.Entity.Contents;
using AT.Model.Contents;
using AT.Entity.ATLogs;
using AT.Model.ATLogs;
using AT.Entity.SystemValues.ATEntities;
using AT.Model.SystemValues.ATEntities;
using AT.Entity.Basics.Teams;
using AT.Model.Basics.Teams;

namespace AthrillAccount.Project.AutoMapperProfile
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            #region Exception

            CreateMap<ATBusinessException, ATBusinessExceptionModel>();
            CreateMap<ATBusinessExceptionMessage, ATBusinessExceptionMessageModel>();

            #endregion


            #region System

            CreateMap<ATEntity, ATEntityModel>();

            CreateMap<ATDataType, ATDataTypeModel>();
            CreateMap<ATDataValue, ATDataValueModel>();

            #endregion

            #region Authentication and User

            CreateMap<AuthenticationResponseEntityModel, AuthenticationResponseModel>();

            CreateMap<User, UserModel>();
            CreateMap<UserLogin, UserLoginModel>();
            CreateMap<UserRoleLink, UserRoleLinkModel>();
            CreateMap<UserRole, UserRoleModel>();
            CreateMap<UserTrackRecord, UserTrackRecordModel>();

            #endregion

            #region Basics

            #region Teams
            CreateMap<TeamCategory, TeamCategoryModel>();
            CreateMap<TeamMember, TeamMemberModel>();
            CreateMap<TeamCategoryMemberLink, TeamCategoryMemberLinkModel>().MaxDepth(1);
            #endregion

            #endregion

            #region Settings

            CreateMap<SiteSetting, SiteSettingModel>();

            CreateMap<EmailSetting, EmailSettingModel>();

            #endregion

            #region Contents

            CreateMap<Content, ContentModel>();
            CreateMap<ContentType, ContentTypeModel>();

            #endregion

            #region ATLogs
            CreateMap<RecordLog, RecordLogModel>();
            #endregion
        }
    }
}
