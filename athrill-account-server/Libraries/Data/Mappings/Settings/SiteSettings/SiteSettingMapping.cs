using AT.Entity.Settings.SiteSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Settings.SiteSettings
{
    public class SiteSettingMapping : IEntityTypeConfiguration<SiteSetting>
    {
        public void Configure(EntityTypeBuilder<SiteSetting> builder)
        {
            builder.ToTable("SiteSettings");
            builder.HasKey(o => o.SiteSettingId);
            builder.Property(t => t.Name)
                    .IsRequired()
                    .HasColumnName("SiteName")
                    .HasMaxLength(200);
            builder.Property(t => t.Name1)
                    .HasColumnName("SiteName1")
                    .HasMaxLength(200);
            builder.Property(t => t.Name2)
                    .HasColumnName("SiteName2")
                    .HasMaxLength(200);
            builder.Property(t => t.Name3)
                    .HasColumnName("SiteName3")
                    .HasMaxLength(200);
            builder.Property(t => t.Name4)
                    .HasColumnName("SiteName4")
                    .HasMaxLength(200);
            builder.Property(t => t.SiteSlogan)
                    .IsRequired()
                    .HasMaxLength(400);
            builder.Property(t => t.SiteUrl)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.ImageName)
                    .IsRequired();
            builder.Property(t => t.FeedbackEmail)
                    .IsRequired();
            builder.Property(t => t.CopyrightName)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.WorkHours)
                    .HasMaxLength(200); 
            builder.Property(t => t.AddressShortDetail)
                    .IsRequired()
                    .HasMaxLength(600);
            builder.Property(t => t.AddressDetail)
                    .IsRequired();
            builder.Property(t => t.PhoneNumber)
                    .IsRequired();
            builder.Property(t => t.MetaTitle)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(t => t.MetaKeywords)
                   .IsRequired()
                   .HasMaxLength(400);
            builder.Property(t => t.MetaDescription)
                   .IsRequired();
            builder.Ignore(i => i.Id);
        }
    }
}
