using AT.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Users
{
    public class UserLoginMapping : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogins");
            builder.HasKey(o => o.UserLoginId);
            builder.Property(t => t.UserId)
                    .IsRequired();
            builder.Property(t => t.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.Password)
                    .IsRequired();
            builder.Property(t => t.SaltKey)
                    .IsRequired();

            builder.HasOne(d => d.User)
                .WithOne(s => s.UserLogin);
        }
    }
}
