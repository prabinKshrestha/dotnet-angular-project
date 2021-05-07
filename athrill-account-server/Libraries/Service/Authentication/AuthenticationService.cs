using AT.Common.Api.Constants;
using AT.Common.Constants;
using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Common.Helpers;
using AT.Common.Infrastructure.Interfaces;
using AT.Common.Models;
using AT.Data.Interface;
using AT.Entity.Authentication;
using AT.Entity.Settings.SiteSettings;
using AT.Entity.Users;
using AT.Service.Authentication.Rules;
using AT.Service.Settings.SiteSettings;
using AT.Service.System.SendMails;
using AT.Service.Users.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AT.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserLogin> _userLoginRepository;
        private readonly IRepository<UserRoleLink> _userRoleLinkRepository;
        private readonly IRepository<UserTrackRecord> _userTrackRecordRepository;
        private readonly ISendMailService _sendMailService;
        private readonly ISiteSettingService _siteSettingService;
        private readonly IUserServiceRule _userServiceRule;
        private readonly IAuthenticationServiceRule _authenticationServiceRule;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly IClientInfoContext _clientInfoContext;
        public AuthenticationService(IRepository<User> userRepository
            , IRepository<UserLogin> userLoginRepository
            , IRepository<UserRoleLink> userRoleLinkRepository
            , IRepository<UserTrackRecord> userTrackRecordRepository
            , ISendMailService sendMailService
            , ISiteSettingService siteSettingService
            , IUserServiceRule userServiceRule
            , IAuthenticationServiceRule authenticationServiceRule
            , IConfiguration configuration
            , IWorkContext workContext
            , IClientInfoContext clientInfoContext
            , IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userLoginRepository = userLoginRepository;
            _userRoleLinkRepository = userRoleLinkRepository;
            _userTrackRecordRepository = userTrackRecordRepository;
            _sendMailService = sendMailService;
            _siteSettingService = siteSettingService;
            _userServiceRule = userServiceRule;
            _authenticationServiceRule = authenticationServiceRule;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _workContext = workContext;
            _clientInfoContext = clientInfoContext;
        }
        public AuthenticationResponseEntityModel Authenticate(AuthenticationRequestEntityModel authRequestEntityModel)
        {
            UserLogin userLogin = _userLoginRepository.TableNotTracked
                                    .FirstOrDefault(x => x.Username == authRequestEntityModel.Username);
            if (userLogin != null)
            {
                User user = _userRepository.TableNotTracked.Include(x => x.Gender)
                                        .FirstOrDefault(x => x.UserId == userLogin.UserId);

                UserRoleLink userRoleLink = _userRoleLinkRepository.TableNotTracked.Include(x => x.UserRole)
                                        .FirstOrDefault(x => x.UserId == userLogin.UserId);

                if (SecurityHelper.GetHashPassword(authRequestEntityModel.Password, userLogin.SaltKey) == userLogin.Password)
                {
                    if (user.IsActive)
                    {

                        byte[] key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("ATAuthentication:ATAuthTokenSecretKey"));
                        int expireTTL = _configuration.GetValue<int>("ATAuthentication:ATAuthTokenExpireTTL");

                        // authentication successful => so generate jwt token
                        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                    new Claim(CustomClaimTypes.CLAIM_USER_ID, user.UserId.ToString()),
                                    new Claim(ClaimTypes.Role, userRoleLink.UserRoleId.ToString()),
                                    new Claim(ClaimTypes.Email, user.Email),
                                    new Claim(ClaimTypes.NameIdentifier, userLogin.Username),
                                    new Claim(ClaimTypes.PrimarySid, userLogin.UserLoginId.ToString()),
                            }),
                            Expires = DateTime.UtcNow.AddHours(expireTTL),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        string Token = tokenHandler.WriteToken(securityToken);

                        #region WorkContext
                        
                        _workContext.SetWorkContext(new WorkContextModel()
                        {
                            UserId = user.UserId,
                            UserLoginId = userLogin.UserLoginId,
                            UserRoleId = userRoleLink.UserRoleId
                        });

                        #endregion

                        #region set user data track

                        UserTrackRecord userTrackRecord = new UserTrackRecord()
                        {
                            UserId = user.UserId,
                            UserTrackTypeId = (int)UserTrackTypes.Login,
                            IpAddress = authRequestEntityModel.IpAddress,
                            ClientName = authRequestEntityModel.ClientName,
                            CreatedById = userLogin.UserId,
                            CreatedOn = DateTime.UtcNow
                        };

                        _userTrackRecordRepository.Add(userTrackRecord);
                        _unitOfWork.Commit(false); // do not insert logs to record log table

                        #endregion

                        user.UserRoleLink = userRoleLink;
                        user.UserLogin = userLogin;

                        return new AuthenticationResponseEntityModel()
                        {
                            Token = Token,
                            TokenExpireTTL = (DateTime)tokenDescriptor.Expires,
                            IsResetPassword = userLogin.IsResetPassword,
                            User = user,
                            UserRoleId = userRoleLink.UserRoleId,
                            Permissions = GetPermissions()
                        };
                    }
                    else
                    {
                        throw new ATAuthenticationException("You are not active to be in system. Please Inform Super Admin. Please try again later.");
                    }
                }
                else
                {
                    throw new ATAuthenticationException("Invalid Username and Password.");
                }
            }
            else
            {
                throw new ATAuthenticationException("Invalid Username.");
            }
        }

        public void SignOut()
        {
            #region set user data track

                UserTrackRecord userTrackRecord = new UserTrackRecord()
                {
                    UserId = _workContext.UserId,
                    UserTrackTypeId = (int)UserTrackTypes.Logout,
                    IpAddress = _clientInfoContext.IPAddress,
                    ClientName = _clientInfoContext.GetClientInfoName(),
                    CreatedById = _workContext.UserId,
                    CreatedOn = DateTime.UtcNow
                };

                _userTrackRecordRepository.Add(userTrackRecord);
                _unitOfWork.Commit(false); // do not insert logs to record log table

            #endregion
        }

        public User RegisterUser(UserRegistrationEntityModel entity)
        {
            User userInformation = new User();
            UserLogin userLogin = new UserLogin();
            UserRoleLink userRoleLink = new UserRoleLink();

            #region assigning values

            // user information
            userInformation.FirstName = entity.FirstName;
            userInformation.MiddleName = entity.MiddleName;
            userInformation.LastName = entity.LastName;
            userInformation.Email = entity.Email;
            userInformation.PhoneNumber = entity.PhoneNumber;
            userInformation.Address = entity.Address;
            userInformation.DOB = entity.DOB;
            userInformation.GenderId = entity.GenderId;
            userInformation.IsActive = entity.IsActive;
            userInformation.ImageName = entity.ImageName;

            // user role
            userRoleLink.UserRoleId = entity.UserRoleId;

            // user login
            userLogin.Username = entity.Username;
            userLogin.IsResetPassword = true;

            #endregion

            #region Business Rule

            _authenticationServiceRule.CheckUserRegistrationRule(entity);
           
            #endregion

            #region add User

            _userRepository.Add(userInformation);
            _unitOfWork.Commit();

            #endregion

            #region Assign User Id to other entity

            userRoleLink.UserId = userInformation.UserId; //userrolelink
            userLogin.UserId = userInformation.UserId; //userlogin

            #endregion

            #region set userlogin

            Guid guid = Guid.NewGuid();
            userLogin.SaltKey = guid.ToString();
            string tempPassword = GetTemporaryPassword();
            userLogin.Password = SecurityHelper.GetHashPassword(tempPassword, guid.ToString());

            #endregion

            #region add userlogin and userrolelink

            _userLoginRepository.Add(userLogin);
            _userRoleLinkRepository.Add(userRoleLink);
            _unitOfWork.Commit();

            #endregion

            #region set user data track

            UserTrackRecord userTrackRecord = new UserTrackRecord()
            {
                UserId = userInformation.UserId,
                UserTrackTypeId = (int)UserTrackTypes.RegisterUser,
                IpAddress = _clientInfoContext.IPAddress,
                ClientName = _clientInfoContext.GetClientInfoName(),
                CreatedById = _workContext.UserId,
                CreatedOn = DateTime.UtcNow
            };

            _userTrackRecordRepository.Add(userTrackRecord);
            _unitOfWork.Commit(false); // do not insert logs to record log table

            #endregion

            #region send mail

            User loggedInUser = _userRepository.TableNotTracked.FirstOrDefault(x => x.UserId == _workContext.UserId);
            SiteSetting siteSetting = _siteSettingService.GetSiteSetting();

            Task.Factory.StartNew(() => {
                _sendMailService.SendMailToUser(new SendMailModel()
                {
                    ToName = $"{entity.FirstName} {entity.MiddleName} {entity.LastName}",
                    ToEmail = entity.Email,
                    Subject = "User Registration",
                    TextBody = $"You are registered as a {((UserRoles)entity.UserRoleId).GetDisplayName()}. Username: {entity.Username} Password : {tempPassword}. ",
                    IsHtml = true,
                    Content = new SendMailContentModel()
                    {
                        Title = $"Dear { entity.FirstName } { entity.MiddleName } { entity.LastName },",
                        Bodies = new List<string>()
                        {
                           $"{loggedInUser.FirstName} {loggedInUser.MiddleName} {loggedInUser.LastName} has registered you.",
                           $"Username: {entity.Username}",
                           $"Password: {tempPassword}",
                           $"User Role: {((UserRoles)entity.UserRoleId).GetDisplayName()}",
                        }
                    },
                    Button = new SendMailButtonModel()
                    {
                        Name = "Go to Support Site",
                        Url = $"{siteSetting.SiteUrl}/backend"
                    }
                });
            });

            Task.Factory.StartNew(() => {
                _sendMailService.SendMailToUser(new SendMailModel()
                {
                    ToName = $"{loggedInUser.FirstName} {loggedInUser.MiddleName} {loggedInUser.LastName}",
                    ToEmail = loggedInUser.Email,
                    Subject = "User Registration",
                    TextBody = $"You have registered User as a {((UserRoles)entity.UserRoleId).GetDisplayName()}. Username: {entity.Username}. Name: {entity.FirstName} {entity.MiddleName} {entity.LastName}",
                    IsHtml = true,
                    Content = new SendMailContentModel()
                    {
                        Title = $"Dear {loggedInUser.FirstName} {loggedInUser.MiddleName} {loggedInUser.LastName},",
                        Bodies = new List<string>()
                        {
                           $"You have registered user .",
                           $"Username: {entity.Username}",
                           $"Name: {entity.FirstName} {entity.MiddleName} {entity.LastName}",
                           $"User Role: {((UserRoles)entity.UserRoleId).GetDisplayName()}",
                        }
                    }
                });
            });


            Task.Factory.StartNew(() => {
                _sendMailService.SendMailToSystem(new SendMailModel()
                {
                    Subject = "User Registration",
                    TextBody = $"{loggedInUser.FirstName} {loggedInUser.MiddleName} {loggedInUser.LastName} have registered User as a {((UserRoles)entity.UserRoleId).GetDisplayName()}. Username: {entity.Username}. Name: {entity.FirstName} {entity.MiddleName} {entity.LastName}",
                    IsHtml = true,
                    Content = new SendMailContentModel()
                    {
                        Bodies = new List<string>()
                        {
                           $"{loggedInUser.FirstName} {loggedInUser.MiddleName} {loggedInUser.LastName} have registered user.",
                           $"Name: {entity.FirstName} {entity.MiddleName} {entity.LastName}",
                           $"Username: {entity.Username}",
                           $"User Role: {((UserRoles)entity.UserRoleId).GetDisplayName()}",
                        }
                    }
                });
            });

            #endregion

            return userInformation;
        }

        public void ResetPassword(ResetPasswordEnityModel resetPasswordEnityModel)
        {
            _authenticationServiceRule.CheckResetPasswordRule(resetPasswordEnityModel);

            User user = _userRepository.TableNotTracked.FirstOrDefault(x => x.Email == resetPasswordEnityModel.Email);
            UserLogin userLogin = _userLoginRepository.TableNotTracked.FirstOrDefault(x => x.Username == resetPasswordEnityModel.Username);

            string tempPassword = GetTemporaryPassword();

            userLogin.OldPassword = userLogin.Password;
            userLogin.Password = SecurityHelper.GetHashPassword(tempPassword, userLogin.SaltKey);
            userLogin.IsResetPassword = true;

            _userLoginRepository.Update(userLogin);
            _unitOfWork.Commit();

            #region Send Mail Reset Password

            SiteSetting siteSetting = _siteSettingService.GetSiteSetting();

            _sendMailService.SendMailToUser(new SendMailModel()
            {
                ToName = $"{user.FirstName} {user.MiddleName} {user.LastName},",
                ToEmail = user.Email,
                Subject = "Reset Password",
                TextBody = $"Password is sucessfully reset. New Password : {tempPassword}",
                IsHtml = true,
                Content = new SendMailContentModel()
                {
                    Title = $"Dear {user.FirstName} {user.MiddleName} {user.LastName}",
                    Bodies = new List<string>()
                    {
                        $"You have Successfully Reset Password.",
                        $"Password: {tempPassword}"
                    }
                },
                Button = new SendMailButtonModel()
                {
                    Name = "Go to Support Site",
                    Url = $"{siteSetting.SiteUrl}/backend"
                }
            });

            _sendMailService.SendMailToSystem(new SendMailModel()
            {
                Subject = "Reset Password",
                TextBody = $"User Reset Password. User : {user.FirstName} {user.MiddleName} {user.LastName}. Email : {user.Email}",
                IsHtml = true,
                Content = new SendMailContentModel()
                {
                    Bodies = new List<string>()
                    {
                        $"User resets password",
                        $"User:  {user.FirstName} {user.MiddleName} {user.LastName}",
                        $"Email: {user.Email}",
                    }
                },
            });

            #endregion

            #region set user data track

            UserTrackRecord userTrackRecord = new UserTrackRecord()
            {
                UserId = userLogin.UserId,
                UserTrackTypeId = (int)UserTrackTypes.ResetPassword,
                IpAddress = resetPasswordEnityModel.IpAddress,
                ClientName = resetPasswordEnityModel.ClientName,
                CreatedById = userLogin.UserId,
                CreatedOn = DateTime.UtcNow
            };

            _userTrackRecordRepository.Add(userTrackRecord);
            _unitOfWork.Commit(false); // do not insert logs to record log table

            #endregion
        }


        public void ChangePassword(ChangePasswordEntityModel changePasswordEntityModel)
        {
            _authenticationServiceRule.CheckChangePasswordRule(changePasswordEntityModel);
            UserLogin userLogin = _userLoginRepository.TableNotTracked.FirstOrDefault(x => x.UserLoginId == _workContext.UserLoginId);
            if(userLogin == null)
            {
                throw new ATReferenceException(_workContext.UserLoginId, nameof(UserLogin));
            }
            userLogin.OldPassword = userLogin.Password;
            userLogin.Password = SecurityHelper.GetHashPassword(changePasswordEntityModel.NewPassword, userLogin.SaltKey);
            userLogin.IsResetPassword = null;
            _userLoginRepository.Update(userLogin);
            _unitOfWork.Commit();

            #region set user data track

            UserTrackRecord userTrackRecord = new UserTrackRecord()
            {
                UserId = userLogin.UserId,
                UserTrackTypeId = (int)UserTrackTypes.ChangePassword,
                IpAddress = _clientInfoContext.IPAddress,
                ClientName = _clientInfoContext.GetClientInfoName(),
                CreatedById = userLogin.UserId,
                CreatedOn = DateTime.UtcNow
            };

            _userTrackRecordRepository.Add(userTrackRecord);
            _unitOfWork.Commit(false); // do not insert logs to record log table

            #endregion
        }

        #region private methods

        private string GetTemporaryPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        private List<string> GetPermissions()
        {
            List<string> retVal = new List<string>();
            retVal.AddRange(GetFeatures());
            retVal.AddRange(GetUserRights());
            return retVal;
        }

        private List<string> GetFeatures()
        {
            List<string> retVal = new List<string>();
            if (AppSettings.Features.SiteSetting)
            {
                retVal.Add(ATPermissions.FEATURE_SITE_SETTING);
            }
            if (AppSettings.Features.EmailSetting)
            {
                retVal.Add(ATPermissions.FEATURE_EMAIL_SETTING);
            }
            if (AppSettings.Features.User)
            {
                retVal.Add(ATPermissions.FEATURE_USER);
            }
            if (AppSettings.Features.UserTrackRecord)
            {
                retVal.Add(ATPermissions.FEATURE_USER_TRACK_RECORD);
            }
            if (AppSettings.Features.RecordLogRecord)
            {
                retVal.Add(ATPermissions.FEATURE_RECORD_LOG_RECORD);
            }
            return retVal;
        }

        private List<string> GetUserRights()
        {
            return ATPermissions.GetUserRightsByRole((UserRoles)_workContext.UserRoleId);
        }


        #endregion
    }
}