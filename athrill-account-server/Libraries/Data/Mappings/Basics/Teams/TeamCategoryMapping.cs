using AT.Entity.Basics.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Basics.Teams
{
    class TeamCategoryMapping : IEntityTypeConfiguration<TeamCategory>
    {
        public void Configure(EntityTypeBuilder<TeamCategory> builder)
        {
            builder.ToTable("TeamCategories");
            builder.HasKey(o => o.TeamCategoryId);
            builder.Property(t => t.Name)
                    .IsRequired()
                    .HasColumnName("TeamCategoryName")
                    .HasMaxLength(200);
            builder.Property(t => t.ShortDescription)
                    .HasMaxLength(600);

            builder.HasMany(s => s.TeamCategoryMemberLinks)
                .WithOne(d => d.TeamCategory);

            builder.Ignore(i => i.Id);
        }
    }
}
