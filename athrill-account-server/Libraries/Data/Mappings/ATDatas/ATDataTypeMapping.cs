using AT.Entity.System.ATDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.System.ATDatas
{
    public class ATDataTypeMapping : IEntityTypeConfiguration<ATDataType>
    {
        public void Configure(EntityTypeBuilder<ATDataType> builder)
        {
            builder.ToTable("ATDataTypes");
            builder.HasKey(o => o.ATDataTypeId);
            builder.Property(t => t.NameKey)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.DisplayName)
                    .HasMaxLength(200);
            builder.Property(t => t.Description)
                    .IsRequired();
            builder.Property(t => t.IsActive)
                    .IsRequired();

            builder.HasMany(d => d.ATDataValues)
                .WithOne(s => s.ATDataType);
        }
    }
}