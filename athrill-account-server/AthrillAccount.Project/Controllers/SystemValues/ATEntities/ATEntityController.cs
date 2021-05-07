using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Api.Attributes;
using AT.Common.Api.Infrastructure;
using AT.Entity.SystemValues.ATEntities;
using AT.Model.SystemValues.ATEntities;
using AT.Service.SystemValues.ATEntities;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Mvc;


namespace AthrillAccount.Project.Controllers.SystemValues.ATEntities
{
    [Route("entity")]
    [ApiController]
    public class ATEntityController : ApiBaseController
    {
        private readonly IATEntityService _aTEntityService;
        public ATEntityController(IATEntityService aTEntityService)
        {
            _aTEntityService = aTEntityService;
        }

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Entity</returns>
        [HttpGet("{id}", Name = "GetEntityRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand, ODataCapabilities.NoFilter)]
        [ODataQueryFilter]
        public ActionResult<ATEntityModel> GetEntity(int id)
        {
            return RequestGetResponse<ATEntity, ATEntityModel>(_aTEntityService, id);
        }

        /// <summary>
        /// Get All Entities
        /// </summary>
        /// <returns>All Entities</returns>
        [HttpGet, HttpHead]
        [BasicAuthorization]
        [ODataQueryCapabilities(ODataCapabilities.NoExpand)]
        [ODataQueryFilter]
        public ActionResult<List<ATEntityModel>> GetEntities()
        {
            return RequestGetResponse<ATEntity, ATEntityModel>(_aTEntityService);
        }

    }
}
