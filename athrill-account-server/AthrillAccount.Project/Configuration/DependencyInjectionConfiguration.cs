using AT.Data;
using AT.Data.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Service.Authentication;
using AT.Service.Users;
using AT.Service.Users.Rules;
using AT.Service.Settings.Rule;
using AT.Service.Settings.EmailSettings;
using AT.Service.Settings.EmailSettings.Rule;
using AT.Service.Settings.SiteSettings;
using AT.Service.Settings.SiteSettings.Rule;
using AT.Service.ATDatas;
using AT.Service.Contents;
using AT.Service.Contents.Rule;
using AT.Service.Contents.ContentTrees;
using AT.Service.System.SendMails;
using AT.Service.Authentication.Rules;
using AT.Service.System.Loggers;
using AT.Service.System.Loggers.FileLoggers;
using AT.Service.ATLogs;
using AT.Common.Infrastructure.Interfaces;
using AT.Common.Infrastructure;
using AT.Service.SystemValues.ATEntities;
using AT.Service.Basics.Teams;

namespace AthrillAccount.Project.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void DependencyInjectionConfig(this IServiceCollection services)
        {
            #region Configuration

            services.AddScoped<IClientInfoContext, ClientInfoContext>();
            services.AddScoped<IAthrillTechDbContext, AthrillTechDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWorkContext, WorkContext>();

            #endregion

            #region Authentication and User

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationServiceRule, AuthenticationServiceRule>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserServiceRule, UserServiceRule>();

            #endregion

            #region System

            services.AddScoped<ISendMailService, SendMailService>();
            services.AddScoped<IATLogger, ATLogger>();
            services.AddScoped<IFileLoggerService, FileLoggerService>();

            #endregion

            #region Services

            #region System Values

            services.AddScoped<IATEntityService, ATEntityService>();

            #endregion

            #region ATDatas

            services.AddScoped<IATDatasService, ATDatasService>();

            #endregion

            #region basics

            #region Teams
            services.AddScoped<ITeamCategoryService, TeamCategoryService>();
            services.AddScoped<ITeamCategoryServiceRule, TeamCategoryServiceRule>();

            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<ITeamMemberServiceRule, TeamMemberServiceRule>();
            #endregion

            #endregion

            #region settings

            services.AddScoped<ISiteSettingService, SiteSettingService>();
            services.AddScoped<ISiteSettingServiceRule, SiteSettingServiceRule>();

            services.AddScoped<IEmailSettingService, EmailSettingService>();
            services.AddScoped<IEmailSettingServiceRule, EmailSettingServiceRule>();

            #endregion

            #region Contents

            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IContentServiceRule, ContentServiceRule>();
            services.AddScoped<IContentTreeService, ContentTreeService>();

            #endregion

            #region ATLogs
            services.AddScoped<IRecordLogService, RecordLogService>();
            #endregion

            #endregion
        }
    }
}
