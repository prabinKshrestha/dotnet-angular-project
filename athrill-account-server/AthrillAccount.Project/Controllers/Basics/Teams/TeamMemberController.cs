using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT.Common.Api.Attributes;
using AT.Entity.Basics.Teams;
using AT.Model.Basics.Teams;
using AT.Service.Basics.Teams;
using AthrillAccount.Project.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthrillAccount.Project.Controllers.Basics.Teams
{
    [Route("teammember")]
    [ApiController]
    public class TeamMemberController : ApiBaseController
    {
        private readonly ITeamMemberService _teamMemberService;
        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        /// <summary>
        ///  Get Team Member
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Team Member</returns>
        [HttpGet("{id}", Name = "GetTeamMemberRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<TeamMemberModel> GetTeamMember(int id)
        {
            return RequestGetResponse<TeamMember, TeamMemberModel>(_teamMemberService, id);
        }

        /// <summary>
        ///  Get All Team Members
        /// </summary>
        /// <returns>All Team Members</returns>
        [HttpGet, HttpHead]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<List<TeamMemberModel>> GetTeamMembers()
        {
            return RequestGetResponse<TeamMember, TeamMemberModel>(_teamMemberService);
        }

        /// <summary>
        ///  Add Team Member
        /// </summary>
        /// <returns>Added Team Member</returns>
        [HttpPost]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<TeamMemberModel> AddTeamMember([FromForm] TeamMemberAddModel model)
        {
            return SimpleAddResponse<TeamMember, TeamMemberModel, TeamMemberAddModel>(model, _teamMemberService);
        }

        /// <summary>
        ///  Update Team Member
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Updated Team Member</returns>
        [HttpPut("{id}", Name = "UpdateTeamMemberRoute")]
        [BasicAuthorization]
        [ODataQueryCapabilities]
        [ODataQueryFilter]
        public ActionResult<TeamMemberModel> UpdateTeamMember(int id, [FromForm] TeamMemberUpdateModel model)
        {
            return SimpleUpdateResponse<TeamMember, TeamMemberModel, TeamMemberUpdateModel>(id, model, _teamMemberService);
        }

        /// <summary>
        ///  Delete Team Member
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Updated Team Member</returns>
        [HttpDelete("{id}", Name = "DeleteTeamMemberRoute")]
        [BasicAuthorization]
        public ActionResult<TeamMemberModel> DeleteTeamMember(int id)
        {
            return SimpleDeleteResponse<TeamMember, TeamMemberModel>(id, _teamMemberService);
        }

        /// <summary>
        ///  Update Position
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateposition", Name = "UpdateTeamMemberPositionRoute")]
        [BasicAuthorization]
        public ActionResult UpdateTeamMemberPosition(List<TeamMemberOrientationUpdateEntityModel> model)
        {
            return ResponseWrapper(() =>
            {
                _teamMemberService.UpdateTeamMemberPostion(model);
                return Ok();
            });
        }
    }
}
