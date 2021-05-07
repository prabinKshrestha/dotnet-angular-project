using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Basics.Teams
{
    public class TeamCategory : BaseEntity, ISoftDeleteEntity
    {
        [RecordLogIgnore]
        public int TeamCategoryId { get; set; }
        public int Orientation { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }

        #region Override

        public override int Id => TeamCategoryId;
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Links

        public virtual ICollection<TeamCategoryMemberLink> TeamCategoryMemberLinks { get; set; }

        #endregion
    }
}
