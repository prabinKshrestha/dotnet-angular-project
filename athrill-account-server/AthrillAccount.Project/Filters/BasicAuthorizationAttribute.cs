using AT.Common.Constants;
using AT.Common.Enum;
using AT.Common.Models;
using AT.Data.Interface;
using AT.Entity.Users;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wangkanai.Detection;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AT.Service.System.Loggers;
using AT.Common.Exceptions;
using System.Text;
using AT.Common.Helpers;
using AT.Common.Infrastructure.Interfaces;

namespace AthrillAccount.Project.Filters
{
    public class BasicAuthorizationAttribute : TypeFilterAttribute
    {
        public BasicAuthorizationAttribute(params ForbidUserRoles[] forbidUserRoles) : base(typeof(BasicAuthorizationActionFilter))
        {
        }
    }

    public class BasicAuthorizationActionFilter : IAuthorizationFilter
    {
        private IRepository<UserRoleLink> _userRoleLinkRepository;
        private readonly IWorkContext _workContext;
        private readonly IClientInfoContext _clientInfoContext;
        private readonly IATLogger _logger;
        public BasicAuthorizationActionFilter(IRepository<UserRoleLink> userRoleLinkRepository
            , IWorkContext workContext
            , IClientInfoContext clientInfoContext
            , IATLogger logger)
        {
            _workContext = workContext;
            _clientInfoContext = clientInfoContext;
            _userRoleLinkRepository = userRoleLinkRepository;
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_clientInfoContext.ClientApplication == ClientApplication.Client) // allow client side
            {
            }
            else if (_clientInfoContext.ClientApplication == ClientApplication.Mobile) // allow mobile client side
            {
            }
            else if(_clientInfoContext.ClientApplication == ClientApplication.SupportSite)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized); // return 401 not authorized
                }
                else
                {
                    int userId = int.Parse(context.HttpContext.User.FindFirst(CustomClaimTypes.CLAIM_USER_ID).Value);
                    int userLoginId = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.PrimarySid).Value);

                    //though role is in claims, it is fetched from table so that latest new record is pulled, because within token active, role can be changed from system.
                    UserRoleLink userRoleLink = _userRoleLinkRepository.TableNotTracked.FirstOrDefault(x => x.UserId == userId);

                    #region Role Management

                    // check for forbidUserRoles Filter
                    CustomAttributeData forbidUserRolesAttributeData = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(BasicAuthorizationAttribute));
                    ReadOnlyCollection<CustomAttributeTypedArgument> rawForbidUserRolesParams = (ReadOnlyCollection<CustomAttributeTypedArgument>)forbidUserRolesAttributeData.ConstructorArguments.FirstOrDefault(y => y.ArgumentType == typeof(ForbidUserRoles[])).Value;
                    List<ForbidUserRoles> forbidUserRolesParams = rawForbidUserRolesParams != null ? rawForbidUserRolesParams.Select(y => (ForbidUserRoles)y.Value).ToList() : new List<ForbidUserRoles>();

                    if (forbidUserRolesParams.Any(x => x == (ForbidUserRoles)userRoleLink.UserRoleId))
                    {
                        context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);

                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine("User is not allowed to use this feature.");
                        stringBuilder.AppendLine($"User Id: {userId}");
                        stringBuilder.AppendLine($"Username: {context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value}");
                        stringBuilder.AppendLine($"User Email: {context.HttpContext.User.FindFirst(ClaimTypes.Email).Value}");
                        stringBuilder.AppendLine($"User Role Id: {userRoleLink.UserRoleId} ({((UserRoles)userRoleLink.UserRoleId).GetDisplayName()})");

                        _logger.LogExceptionToFile(ATExceptionTypes.ATUserRoleForbiddenException,
                            "Authentication Exception : Forbidden Roles.",
                            new ATAuthenticationException(stringBuilder.ToString()),
                            ATErrorLevel.Information);
                    }

                    #endregion

                    #region Work Context

                    WorkContextModel workContextModel = new WorkContextModel()
                    {
                        UserId = userId,
                        UserLoginId = userLoginId,
                        UserRoleId = userRoleLink.UserRoleId
                    };
                    _workContext.SetWorkContext(workContextModel);

                    #endregion
                }
            }
            else
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized); // return 401 not authorized
            }
        }
    }
}