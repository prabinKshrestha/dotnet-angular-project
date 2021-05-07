using AT.Entity.System.ATDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.System.ATDatas
{
    public class ATDataValueMapping : IEntityTypeConfiguration<ATDataValue>
    {
        public void Configure(EntityTypeBuilder<ATDataValue> builder)
        {
            builder.ToTable("ATDataValues");
            builder.HasKey(o => o.ATDataValueId);
            builder.Property(t => t.ATDataTypeId)
                    .IsRequired();
            builder.Property(t => t.DisplayName)
                    .HasMaxLength(200);
            builder.Property(t => t.Value)
                    .IsRequired();
            builder.Property(t => t.Description)
                    .IsRequired();
            builder.Property(t => t.IsActive)
                    .IsRequired();

            builder.HasOne(d => d.ATDataType)
                .WithMany(s => s.ATDataValues);
        }
    }
}