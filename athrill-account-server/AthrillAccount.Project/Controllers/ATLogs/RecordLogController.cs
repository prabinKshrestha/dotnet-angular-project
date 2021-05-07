using AT.Common.Api.Attributes;
using AT.Common.Api.Infrastructure;
using AT.Entity.ATLogs;
using AT.Model.ATLogs;
using AT.Service.ATLogs;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Controllers.ATLogs
{
    [Route("recordlog")]
    [ApiController]
    public class RecordLogController : ApiBaseController
    {
        private readonly IRecordLogService _recordLogService;
        private readonly IConfiguration _configuration;
        public RecordLogController(IRecordLogService recordLogService
            , IConfiguration configuration)
        {
            _recordLogService = recordLogService;
            _configuration = configuration;
        }

        /// <summary>
        /// Get All Record Logs
        /// </summary>
        /// <returns>All Record Logs</returns>
        [HttpGet, HttpHead]
        [BasicAuthorization]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand)]
        [ODataQueryFilter]
        public ActionResult<List<RecordLogModel>> GetRecordLogs()
        {
            return RequestGetResponse<RecordLog, RecordLogModel>(_recordLogService);
        }

        /// <summary>
        /// Get Record Log
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single RecordLog</returns>
        [HttpGet("{id}",Name = "GetRecordLogRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand, ODataCapabilities.NoFilter)]
        [ODataQueryFilter]
        public ActionResult<RecordLogModel> GetRecordLog(int id)
        {
            return RequestGetResponse<RecordLog, RecordLogModel>(_recordLogService, id);
        }

        /// <summary>
        /// Get Last Record Log
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single RecordLog</returns>
        [HttpGet]
        [Route("{id}/getlastrecordlog", Name = "GetLastRecordLogRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand, ODataCapabilities.NoFilter)]
        [ODataQueryFilter]
        public ActionResult<RecordLogModel> GetLastRecordLog(int id)
        {
            return _mapper.Map<RecordLog, RecordLogModel>(_recordLogService.GetLastRecordLog(id));
        }
    }
}
