using AT.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Users
{
    class UserTrackRecordMapping : IEntityTypeConfiguration<UserTrackRecord>
    {
        public void Configure(EntityTypeBuilder<UserTrackRecord> builder)
        {
            builder.ToTable("UserTrackRecords");
            builder.HasKey(o => o.UserTrackRecordId);
            builder.Property(t => t.UserId)
                    .IsRequired();
            builder.Property(t => t.UserTrackTypeId)
                    .IsRequired();
            builder.Property(t => t.IpAddress)
                    .IsRequired();
            builder.Property(t => t.ClientName)
                    .IsRequired();
            builder.Property(t => t.CreatedById)
                    .IsRequired();
            builder.Property(t => t.CreatedOn)
                    .IsRequired();
            
            builder.HasOne(d => d.User)
                .WithMany(t => t.UserTrackRecords);
        }
    }
}
