using AT.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Mappings.Users
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(o => o.UserId);
            builder.Property(t => t.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.MiddleName)
                    .HasMaxLength(50);
            builder.Property(t => t.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(t => t.Email)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(t => t.ImageName)
                    .IsRequired();
            builder.Property(t => t.DOB)
                    .IsRequired();
            builder.Property(t => t.GenderId)
                    .IsRequired();
            builder.Property(t => t.PhoneNumber)
                    .IsRequired();
            builder.Property(t => t.Address)
                    .IsRequired();
            builder.Property(t => t.IsActive)
                    .IsRequired();

            builder.HasOne(d => d.UserLogin)
                .WithOne(s => s.User);
            builder.HasOne(d => d.UserRoleLink)
                .WithOne(s => s.User);
            builder.HasMany(d => d.UserTrackRecords)
                .WithOne(s => s.User);
            builder.HasOne(d => d.Gender)
                .WithMany();
        }
    }
}
