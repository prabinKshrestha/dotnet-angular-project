using AT.Entity.Contents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Contents
{
    public class ContentMapping : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.ToTable("Contents");
            builder.HasKey(o => o.ContentId);
            builder.Property(t => t.ContentTypeId)
                    .IsRequired();
            builder.Property(t => t.ParentId);
            builder.Property(t => t.PlacementId)
                    .IsRequired();
            builder.Property(t => t.Position)
                    .IsRequired();
            builder.Property(t => t.Slug)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.Name)
                    .IsRequired()
                    .HasColumnName("ContentName")
                    .HasMaxLength(200);
            builder.Property(t => t.SubName)
                    .IsRequired()
                    .HasColumnName("ContentSubName")
                    .HasMaxLength(200);
            builder.Property(t => t.ImageAltName)
                    .HasMaxLength(200);
            builder.Property(t => t.ShortDescription)
                    .HasMaxLength(600);
            builder.Property(t => t.IsPublished)
                    .IsRequired();
            builder.HasOne(d => d.ContentType)
                    .WithMany(s => s.Contents);
            builder.HasOne(d => d.Parent)
                    .WithMany(s => s.Childrens);
            builder.HasOne(d => d.Placement)
                    .WithMany();
            builder.Ignore(i => i.Id);
        }
    }
}

