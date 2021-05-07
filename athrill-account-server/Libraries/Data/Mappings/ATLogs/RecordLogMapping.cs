using AT.Entity.ATLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.ATLogs
{
    public class RecordLogMapping : IEntityTypeConfiguration<RecordLog>
    {
        public void Configure(EntityTypeBuilder<RecordLog> builder)
        {
            builder.ToTable("RecordLogs");
            builder.HasKey(t => t.RecordLogId);
            builder.Property(t => t.RecordType)
                .IsRequired();
            builder.Property(t => t.EntityId)
                .IsRequired();
            builder.Property(t => t.Record)
                .IsRequired();
            builder.Property(t => t.CreatedBy)
                .IsRequired();
            builder.Property(t => t.CreatedOn)
                .IsRequired();

            builder.HasOne(x => x.Entity)
                .WithMany();
        }
    }
}
