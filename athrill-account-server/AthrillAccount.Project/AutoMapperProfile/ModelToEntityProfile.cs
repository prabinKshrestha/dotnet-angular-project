using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Entity.Authentication;
using AT.Model.Authentication;
using AT.Model.Settings.EmailSettings;
using AT.Entity.Settings.EmailSettings;
using AT.Model.Settings.SiteSettings;
using AT.Entity.Settings.SiteSettings;
using AT.Model.Contents;
using AT.Entity.Contents;
using AT.Model.Users;
using AT.Entity.Users;
using AT.Model.Basics.Teams;
using AT.Entity.Basics.Teams;

namespace AthrillAccount.Project.AutoMapperProfile
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {

            #region Authorization and User
            CreateMap<UserRegistrationModel, UserRegistrationEntityModel>();
            CreateMap<ChangePasswordModel, ChangePasswordEntityModel>();
            
            CreateMap<UserUpdateModel, User>();

            #endregion

            #region System

            #endregion

            #region Basics

            #region Teams
            CreateMap<TeamCategoryAddModel, TeamCategory>();
            CreateMap<TeamCategoryUpdateModel, TeamCategory>();

            CreateMap<TeamMemberAddModel, TeamMember>();
            CreateMap<TeamMemberUpdateModel, TeamMember>();
            #endregion

            #endregion

            #region Settings


            CreateMap<SiteSettingUpdateModel, SiteSetting>();
            CreateMap<EmailSettingAddModel, EmailSetting>();
            CreateMap<EmailSettingUpdateModel, EmailSetting>();

            #endregion

            #region Contents

            CreateMap<ContentAddModel, Content>();
            CreateMap<ContentUpdateModel, Content>();

            #endregion

            #region Users
            CreateMap<UserRegisteredUpdateModel, UserRegisteredUpdateEntityModel>();
            #endregion
        }
    }
}
