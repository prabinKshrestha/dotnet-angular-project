using AT.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Entity.Basics.Teams
{
    public class TeamMember : BaseEntity, ISoftDeleteEntity, IImageEntity
    {
        [RecordLogIgnore]
        public int TeamMemberId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public string Quotation { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string SkypeLink { get; set; }
        public string LinkedInLink { get; set; }
        public string Twiterlink { get; set; }
        public string Viber { get; set; }
        public bool IsPublished { get; set; }

        #region overrride

        public override int Id => TeamMemberId;
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Links

        public virtual ICollection<TeamCategoryMemberLink> TeamCategoryMemberLinks { get; set; }

        #endregion

        #region Others
        public List<int> TeamCategoryIds { get; set; }

        #endregion
    }
}
