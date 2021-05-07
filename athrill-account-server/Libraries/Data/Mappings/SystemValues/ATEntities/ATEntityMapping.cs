using AT.Entity.SystemValues.ATEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.SystemValues.ATEntities
{
    public class ATEntityMapping : IEntityTypeConfiguration<ATEntity>
    {
        public void Configure(EntityTypeBuilder<ATEntity> builder)
        {
            builder.ToTable("ATEntities");
            builder.HasKey(o => o.EntityId);
            builder.Property(t => t.NameKey)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.DisplayName)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.Description)
                    .IsRequired();
            builder.Property(t => t.IsShownInRecordLogForSupportSite)
                    .IsRequired();

            builder.Ignore(i => i.Id);
        }
    }
}
