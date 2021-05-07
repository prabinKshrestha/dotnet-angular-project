using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Api.Attributes;
using AT.Common.Api.Helpers;
using AT.Common.Api.Infrastructure;
using AT.Common.Api.Request;
using AT.Common.Enum;
using AT.Entity.Users;
using AT.Model.Users;
using AT.Service.Users;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using static AT.Common.Api.Constants.ApiConstants;

namespace AthrillAccount.Project.Controllers.Users
{
    [Route("user")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        ///  Get User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single User</returns>
        [HttpGet("{id}", Name = "GetUserRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<UserModel> GetUser(int id)
        {
            return RequestGetResponse<User, UserModel>(_userService, id);
        }

        /// <summary>
        ///  Get All Users
        /// </summary>
        /// <returns>All Users</returns>
        [HttpGet, HttpHead]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<List<UserModel>> GetUsers()
        {
            return RequestGetResponse<User, UserModel>(_userService);
        }

        /// <summary>
        ///  Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Updated User</returns>
        [HttpPut("{id}", Name = "UpdateUserRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<UserModel> UpdateUser(int id, [FromForm] UserUpdateModel model)
        {
            return SimpleUpdateResponse<User, UserModel, UserUpdateModel>(id, model, _userService);
        }


        /// <summary>
        ///  Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Updated User</returns>
        [HttpDelete("{id}", Name = "DeleteUserRoute")]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        public ActionResult<UserModel> DeleteUser(int id)
        {
            return SimpleDeleteResponse<User, UserModel>(id, _userService);
        }

        /// <summary>
        ///  Change Active Status of User
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="activestatus"></param>
        /// <returns>HTTP Response Status</returns>
        [HttpPut]
        [Route("{userid}/changeactivestatus/{activestatus}", Name = "ChangeActiveStatusRoute")]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        public ActionResult ChangeActiveStatus(int userid, bool activestatus)
        {
            return ResponseWrapper(() =>
            {
                _userService.ChangeActiveStatus(userid, activestatus);
                return StatusCode(StatusCodes.Status200OK);
            });
        }

        /// <summary>
        ///  Get All Users
        /// </summary>
        /// <returns>All Users</returns>
        [HttpGet, HttpHead]
        [Route("usertrackrecords", Name = "GetUserTrackRecordsRoute")]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<List<UserTrackRecordModel>> GetUserTrackRecords()
        {
            return ResponseWrapper<List<UserTrackRecordModel>>(() => {
                RequestBase requestBase = RequestHelper.GetRequestBase((ODataQueryOptions)HttpContext.Items[RequestProperties.ODATA_OPTIONS]);
                if (HttpMethods.IsHead(Request.Method))
                {
                    // only show return count on response header
                    Response.Headers.Add(ResponseHeaderProperties.COUNT, _userService.GetUserTrackRecordsCount(requestBase).ToString());
                    return Ok();
                }
                return Ok(_mapper.Map<List<UserTrackRecord>,List<UserTrackRecordModel>>(_userService.GetUserTrackRecords(requestBase).ToList()));
            });
        }


        /// <summary>
        ///  Get All User Roles
        /// </summary>
        /// <returns>All Users</returns>
        [HttpGet, HttpHead]
        [Route("roles",Name ="GetUserRolesRoute")]
        [BasicAuthorization]
        public ActionResult<List<UserRoleModel>> GetUserRoles()
        {
            return ResponseWrapper<List<UserRoleModel>>(() =>
            {
                return _mapper.Map<List<UserRole>, List<UserRoleModel>>(_userService.GetUserRoles().ToList());
            });
        }

        /// <summary>
        ///  Update Registered User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns>HTTP Response Status</returns>
        [HttpPut]
        [Route("{userId}/updateregistereduser", Name = "UpdateRegisteredUserRoute")]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        public ActionResult UpdateRegisteredUser(int userId, [FromBody] UserRegisteredUpdateModel model)
        {
            return ResponseWrapper(() =>
            {
                _userService.UpdateRegisteredUser(userId, _mapper.Map<UserRegisteredUpdateEntityModel>(model));
                return StatusCode(StatusCodes.Status200OK);
            });
        }
    }
}
