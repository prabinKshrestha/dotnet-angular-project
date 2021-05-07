using AT.Entity.Contents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Contents
{
    public class ContentTypeMapping : IEntityTypeConfiguration<ContentType>
    {
        public void Configure(EntityTypeBuilder<ContentType> builder)
        {
            builder.ToTable("ContentTypes");
            builder.HasKey(o => o.ContentTypeId);
            builder.Property(t => t.NameKey)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.DisplayName)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.Description)
                    .IsRequired();
            builder.Property(t => t.IsActive)
                    .IsRequired();
            builder.HasMany(x => x.Contents)
                    .WithOne(t => t.ContentType);
            builder.Ignore(i => i.Id);
        }
    }
}
