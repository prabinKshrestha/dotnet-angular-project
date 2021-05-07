using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Api.Attributes;
using AT.Common.Api.Infrastructure;
using AT.Entity.Basics.Teams;
using AT.Model.Basics.Teams;
using AT.Service.Basics.Teams;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AthrillAccount.Project.Controllers.Basics.Teams
{
    [Route("teamcategory")]
    [ApiController]
    public class TeamCategoryController : ApiBaseController
    {
        private readonly ITeamCategoryService _teamCategoryService;
        public TeamCategoryController(ITeamCategoryService teamCategoryService)
        {
            _teamCategoryService = teamCategoryService;
        }

        /// <summary>
        ///  Get Team Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Team Category</returns>
        [HttpGet("{id}", Name = "GetTeamCategoryRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<TeamCategoryModel> GetTeamCategory(int id)
        {
            return RequestGetResponse<TeamCategory, TeamCategoryModel>(_teamCategoryService, id);
        }

        /// <summary>
        ///  Get All Team Categories
        /// </summary>
        /// <returns>All Team Categories</returns>
        [HttpGet, HttpHead]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<List<TeamCategoryModel>> GetTeamCategories()
        {
            return RequestGetResponse<TeamCategory, TeamCategoryModel>(_teamCategoryService);
        }

        /// <summary>
        ///  Add Team Category
        /// </summary>
        /// <returns>Added Team Category</returns>
        [HttpPost]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<TeamCategoryModel> AddTeamCategory([FromBody] TeamCategoryAddModel model)
        {
            return SimpleAddResponse<TeamCategory, TeamCategoryModel, TeamCategoryAddModel>(model, _teamCategoryService);
        }

        /// <summary>
        ///  Update Team Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Updated Team Category</returns>
        [HttpPut("{id}", Name = "UpdateTeamCategoryRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<TeamCategoryModel> UpdateTeamCategory(int id, [FromBody] TeamCategoryUpdateModel model)
        {
            return SimpleUpdateResponse<TeamCategory, TeamCategoryModel, TeamCategoryUpdateModel>(id, model, _teamCategoryService);
        }

        /// <summary>
        ///  Delete Team Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Updated Team Category</returns>
        [HttpDelete("{id}", Name = "DeleteTeamCategoryRoute")]
        [BasicAuthorization]
        public ActionResult<TeamCategoryModel> DeleteTeamCategory(int id)
        {
            return SimpleDeleteResponse<TeamCategory, TeamCategoryModel>(id, _teamCategoryService);
        }

        /// <summary>
        ///  Update Position
        /// </summary>
        /// <param name="teamCategoryIds"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateposition", Name = "UpdateTeamCategoryPositionRoute")]
        [BasicAuthorization]
        public ActionResult UpdateTeamCategoryPosition(List<int> teamCategoryIds)
        {
            return ResponseWrapper(() =>
            {
                _teamCategoryService.UpdateTeamCategoryPostion(teamCategoryIds);
                return Ok();
            });
        }
    }
}
