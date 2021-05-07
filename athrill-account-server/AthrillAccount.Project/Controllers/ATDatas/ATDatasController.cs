using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Entity.System.ATDatas;
using AT.Model.ATDatas;
using AT.Service.ATDatas;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Mvc;


namespace AthrillAccount.Project.Controllers.ATDatas
{
    [Route("atdatas")]
    [ApiController]
    public class ATDatasController : ApiBaseController
    {
        private readonly IATDatasService _atDatasService;

        public ATDatasController(IATDatasService atDatasService)
        {
            _atDatasService = atDatasService;
        }

        /// <summary>
        ///  Get ATDatasValues by ATDataType
        /// </summary>
        /// <returns>List of ATDatas</returns>
        [HttpGet]
        [Route("getatdatavaluesbytype/{id}", Name = "GetATDataValuesByTypeRoute")]
        [BasicAuthorization]
        public ActionResult<List<ATDataValueModel>> GetATDataValuesByType(int id)
        {
            return ResponseWrapper<List<ATDataValueModel>>(() => {
                return _mapper.Map<List<ATDataValue>, List<ATDataValueModel>>(_atDatasService.GetATDataValuesByType(id)); 
            });
        }
    }
}
