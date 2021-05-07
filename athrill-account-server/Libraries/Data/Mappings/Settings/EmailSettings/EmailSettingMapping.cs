using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AT.Entity.Settings.EmailSettings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AT.Data.Mappings.Settings.EmailSettings
{
    class EmailSettingMapping : IEntityTypeConfiguration<EmailSetting>
    {
        public void Configure(EntityTypeBuilder<EmailSetting> builder)
        {
            builder.ToTable("EmailSettings");
            builder.HasKey(o => o.EmailSettingId);
            builder.Property(t => t.Name)
                    .IsRequired()
                    .HasColumnName("EmailName")
                    .HasMaxLength(200);
            builder.Property(t => t.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            builder.Property(t => t.Password)
                    .IsRequired();
            builder.Property(t => t.SendFromName)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.ReplyToName)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.ReplyToEmail)
                    .IsRequired()
                    .HasMaxLength(100);
            builder.Property(t => t.Host)
                    .IsRequired();
            builder.Property(t => t.Port)
                    .IsRequired();
            builder.Property(t => t.IsSSL)
                    .IsRequired();
            builder.Property(t => t.IsDefault)
                    .IsRequired();
            builder.Property(t => t.Description)
                    .IsRequired();
            builder.Property(t => t.IsPublished)
                    .IsRequired();

            builder.Ignore(i => i.Id);
        }
    }
}
