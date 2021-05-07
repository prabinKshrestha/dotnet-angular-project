using AT.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Users
{
    class UserRoleLinkMapping : IEntityTypeConfiguration<UserRoleLink>
    {
        public void Configure(EntityTypeBuilder<UserRoleLink> builder)
        {
            builder.ToTable("UserRoleLinks");
            builder.HasKey(o => o.UserRoleLinkId);
            builder.Property(t => t.UserId)
                    .IsRequired();
            builder.Property(t => t.UserRoleId)
                    .IsRequired();

            builder.HasOne(d => d.User)
                    .WithOne(s => s.UserRoleLink);
            builder.HasOne(d => d.UserRole)
                    .WithMany(s => s.UserRoleLinks);
         }
    }
}
