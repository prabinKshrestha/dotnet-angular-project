using AT.Entity.Basics.Teams;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service.Basics.Teams
{
    public interface ITeamMemberService : IBaseService<TeamMember>, IQueryService<TeamMember>
    {
        void UpdateTeamMemberPostion(List<TeamMemberOrientationUpdateEntityModel> model);
    }
}
