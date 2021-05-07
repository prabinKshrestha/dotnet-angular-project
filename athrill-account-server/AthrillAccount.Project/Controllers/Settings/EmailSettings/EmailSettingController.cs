using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Api.Attributes;
using AT.Common.Api.Infrastructure;
using AT.Common.Enum;
using AT.Entity.Settings.EmailSettings;
using AT.Model.Settings.EmailSettings;
using AT.Service.Settings.EmailSettings;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthrillAccount.Project.Controllers.Settings.EmailSettings
{
    [Route("emailsetting")]
    [ApiController]
    public class EmailSettingController : ApiBaseController
    {
        private readonly IEmailSettingService _emailSettingService;
        public EmailSettingController(IEmailSettingService emailSettingService)
        {
            _emailSettingService = emailSettingService;
        }
        /// <summary>
        ///  Get Email Setting
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Email Setting</returns>
        [HttpGet("{id}", Name = "GetEmailSettingRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand)]
        [ODataQueryFilter]
        public ActionResult<EmailSettingModel> GetEmailSetting(int id)
        {
            return RequestGetResponse<EmailSetting, EmailSettingModel>(_emailSettingService, id);
        }

        /// <summary>
        ///  Get All Email Settings
        /// </summary>
        /// <returns>All Email Settings</returns>
        [HttpGet, HttpHead]
        [BasicAuthorization]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand)]
        [ODataQueryFilter]
        public ActionResult<List<EmailSettingModel>> GetEmailSettings()
        {
            return RequestGetResponse<EmailSetting, EmailSettingModel>(_emailSettingService);
        }

        /// <summary>
        ///  Add Email Setting
        /// </summary>
        /// <returns>Added Email Setting</returns>
        [HttpPost]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand)]
        [ODataQueryFilter]
        public ActionResult<EmailSettingModel> AddEmailSetting([FromBody] EmailSettingAddModel model)
        {
            return SimpleAddResponse<EmailSetting, EmailSettingModel, EmailSettingAddModel>(model, _emailSettingService);
        }

        /// <summary>
        ///  Update Email Setting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Updated Email Setting</returns>
        [HttpPut("{id}", Name = "UpdateEmailSettingRoute")]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand)]
        [ODataQueryFilter]
        public ActionResult<EmailSettingModel> UpdateEmailSetting(int id, [FromBody] EmailSettingUpdateModel model)
        {
            return SimpleUpdateResponse<EmailSetting, EmailSettingModel, EmailSettingUpdateModel>(id, model, _emailSettingService);
        }

        /// <summary>
        ///  Delete Email Setting
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Updated Email Setting</returns>
        [HttpDelete("{id}", Name = "DeleteEmailSettingRoute")]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        public ActionResult<EmailSettingModel> DeleteEmailSetting(int id)
        {
            return SimpleDeleteResponse<EmailSetting, EmailSettingModel>(id, _emailSettingService);
        }

        /// <summary>
        ///  Change Default Status
        /// </summary>
        /// <returns>Changed Default Status</returns>
        [HttpPut]
        [Route("{emailId}/changedefaultstatus/{defaultStatus}", Name = "ChangeDefaultStatusRoute")]
        [BasicAuthorization(ForbidUserRoles.NoNormal)]
        public ActionResult ChangeDefaultStatus(int emailId, bool defaultStatus)
        {
            return ResponseWrapper(() => {
                _emailSettingService.ChangeDefaultStatus(emailId, defaultStatus);
                return StatusCode(StatusCodes.Status200OK);
            });
        }
    }
}