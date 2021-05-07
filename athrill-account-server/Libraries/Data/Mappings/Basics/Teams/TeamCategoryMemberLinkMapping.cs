using AT.Entity.Basics.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Basics.Teams
{
    class TeamCategoryMemberLinkMapping : IEntityTypeConfiguration<TeamCategoryMemberLink>
    {
        public void Configure(EntityTypeBuilder<TeamCategoryMemberLink> builder)
        {
            builder.ToTable("TeamCategoryMemberLinks");
            builder.HasKey(o => o.TeamCategoryMemberLinkId);

            builder.HasOne(s => s.TeamCategory)
                .WithMany(d => d.TeamCategoryMemberLinks);

            builder.HasOne(s => s.TeamMember)
                .WithMany(d => d.TeamCategoryMemberLinks);

            builder.Ignore(i => i.Id);
        }
    }
}
