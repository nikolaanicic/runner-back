using Entities.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;


namespace Entities.ModelConfigurations.UsersConfiguration
{
    public abstract class BaseUserConfiguration<UserType> : IEntityTypeConfiguration<UserType>
        where UserType :User
    {
        public abstract void Configure(EntityTypeBuilder<UserType> builder);

        protected void ConfigureCommon(EntityTypeBuilder<UserType> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.DateOfBirth).IsRequired();

            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasOne(u => u.Role)
                .WithMany(r => (IEnumerable<UserType>)r.UsersInRole)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
