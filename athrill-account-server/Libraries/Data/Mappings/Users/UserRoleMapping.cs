using AT.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Users
{
    class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(o => o.UserRoleId);
            builder.Property(t => t.NameKey)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.DisplayName)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.Description)
                    .IsRequired();
            builder.Property(t => t.IsShown)
                    .IsRequired();

            builder.HasMany(d => d.UserRoleLinks)
                .WithOne(s => s.UserRole);
        }
    }
}
