using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Basics.Teams
{
    public class TeamCategoryMemberLink : BaseEntity, ISoftDeleteEntity
    {
        [RecordLogIgnore]
        public int TeamCategoryMemberLinkId { get; set; }

        [RecordLogIgnore]
        public int TeamCategoryId { get; set; }

        [RecordLogIgnore]
        public int TeamMemberId { get; set; }

        public int TeamMemberOrientation { get; set; }

        #region Override

        public override int Id => TeamCategoryMemberLinkId;

        #endregion


        #region Links
        [RecordLogIgnore]

        public virtual TeamCategory TeamCategory { get; set; }

        [RecordLogIgnore]
        public virtual TeamMember TeamMember { get; set; }

        #endregion
    }
}
