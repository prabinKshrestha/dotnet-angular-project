using AT.Entity.Basics.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Basics.Teams
{
    class TeamMemberMapping : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.ToTable("TeamMembers");
            builder.HasKey(o => o.TeamMemberId);
            builder.Property(t => t.Name)
                    .IsRequired()
                    .HasColumnName("TeamMemberName")
                    .HasMaxLength(200);
            builder.Property(t => t.Role)
                    .HasMaxLength(400);
            builder.Property(t => t.Quotation)
                    .HasMaxLength(400);
            builder.Property(t => t.Position)
                    .HasMaxLength(400);
            builder.Property(t => t.ShortDescription)
                    .HasMaxLength(600);
            builder.Property(t => t.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            builder.Property(t => t.ImageName)
                    .IsRequired();

            builder.HasMany(s => s.TeamCategoryMemberLinks)
                .WithOne(d => d.TeamMember);

            builder.Ignore(i => i.Id);
            builder.Ignore(i => i.TeamCategoryIds);
        }
    }
}
