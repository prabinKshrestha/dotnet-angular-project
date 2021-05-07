using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Enum;
using AT.Common.Exceptions;
using AT.Common.Infrastructure.Interfaces;
using AT.Entity;
using AT.Entity.Authentication;
using AT.Entity.Users;
using AT.Model;
using AT.Model.Authentication;
using AT.Model.Users;
using AT.Service.Authentication;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AthrillAccount.Project.Controllers.Authentication
{
    [Route("authentication")]
    [ApiController]
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IClientInfoContext _clientInfoContext;
        public AuthenticationController(IAuthenticationService authenticationService
            , IClientInfoContext clientInfoContext)
        {
            _authenticationService = authenticationService;
            _clientInfoContext = clientInfoContext;
        }

        /// <summary>
        ///  Sign In / Log In
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>Authentication Response</returns>
        [HttpPost]
        [Route("login",Name ="SignInRoute")]
        public ActionResult<AuthenticationResponseModel> SignIn([FromBody] LoginModel loginModel)
        {
            return ResponseWrapper<AuthenticationResponseModel>(() => {
                AuthenticationRequestEntityModel authenticationRequestEntityModel = new AuthenticationRequestEntityModel()
                {
                    Username = loginModel.Username,
                    Password = loginModel.Password,
                    IpAddress = _clientInfoContext.IPAddress,
                    ClientName = _clientInfoContext.GetClientInfoName()
            };
                return _mapper.Map<AuthenticationResponseModel>(_authenticationService.Authenticate(authenticationRequestEntityModel));
            });
        }

        /// <summary>
        ///  Sign Out / Log Out
        /// </summary>
        [HttpPost]
        [Route("logout", Name = "SignOutRoute")]
        [BasicAuthorization]
        public ActionResult SignOut()
        {
            return ResponseWrapper(() =>
            {
                _authenticationService.SignOut();
                return StatusCode(StatusCodes.Status200OK);
            });
        }

        /// <summary>
        ///  Reset Password
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns>HTTP Response Status</returns>
        [HttpPost]
        [Route("resetpassword", Name = "ResetPasswordRoute")]
        public ActionResult ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            return ResponseWrapper(() =>
            {
                ResetPasswordEnityModel resetPasswordEnityModel = new ResetPasswordEnityModel()
                {
                    Email = resetPasswordModel.Email,
                    Username = resetPasswordModel.Username,
                    IpAddress = _clientInfoContext.IPAddress,
                    ClientName = _clientInfoContext.GetClientInfoName()
                };
                _authenticationService.ResetPassword(resetPasswordEnityModel);
                return StatusCode(StatusCodes.Status200OK);
            });
        }

        /// <summary>
        ///  Change Password
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns>HTTP Response Status</returns>
        [HttpPut]
        [Route("changepassword", Name = "ChangePasswordRoute")]
        [BasicAuthorization] 
        public ActionResult ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            return ResponseWrapper(() =>
            {
                _authenticationService.ChangePassword(_mapper.Map<ChangePasswordModel,ChangePasswordEntityModel>(changePasswordModel));
                return StatusCode(StatusCodes.Status200OK);
            });
        }

        /// <summary>
        ///  Register Users
        /// </summary>
        [HttpPost]
        [Route("register", Name = "RegisterUserRoute")]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        public ActionResult<UserModel> RegisterUser([FromForm] UserRegistrationModel userRegistrationModel)
        {
            return ResponseWrapper<UserModel>(() => {
                UserRegistrationEntityModel entity = _mapper.Map<UserRegistrationEntityModel>(userRegistrationModel);
                ImageModel imageModel = null;
                if (userRegistrationModel.ImageFile != null && userRegistrationModel.ImageFile.Length > 0)
                {
                    string imageName = GenerateImageName(userRegistrationModel.ImageFile);
                    imageModel = new ImageModel(userRegistrationModel.ImageFile, userRegistrationModel.FilePath, imageName);
                    entity.ImageName = imageName;
                }
                else if (userRegistrationModel.IsImageRequired)
                {
                    throw new ATBusinessException("Image is Required.");
                }
                User user = _authenticationService.RegisterUser(entity);
                //save image
                if (imageModel != null)
                {
                    StoreImage(imageModel);
                }
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<UserModel>(user));
            });
        }
    }
}
