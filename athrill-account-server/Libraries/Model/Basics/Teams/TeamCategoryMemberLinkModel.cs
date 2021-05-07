using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model.Basics.Teams
{
    public class TeamCategoryMemberLinkModel : BaseModel
    {
        public int TeamCategoryMemberLinkId { get; set; }
        public int TeamCategoryId { get; set; }
        public int TeamMemberId { get; set; }
        public int TeamMemberOrientation { get; set; }

        #region Override

        public override int Id => TeamCategoryMemberLinkId;

        #endregion


        #region Links

        public TeamCategoryModel TeamCategory { get; set; }
        public TeamMemberModel TeamMember { get; set; }

        #endregion
    }
}
