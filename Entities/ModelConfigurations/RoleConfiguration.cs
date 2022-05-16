using Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.ModelConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();


            builder.Property(r => r.Rolename).IsRequired();
            builder.HasIndex(r => r.Rolename).IsUnique();


            builder.HasData(
                new Role { Id = 1, Rolename = "Admin" },
                new Role { Id = 2, Rolename = "Consumer" },
                new Role { Id = 3, Rolename = "Deliverer" });
        }
    }
}
