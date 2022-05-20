using Contracts.Models;
using Contracts.Security.Passwords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Entities.ModelConfigurations.UsersConfiguration
{
    public class AdminConfiguration : BaseUserConfiguration<Admin>
    {

        private IPasswordHasher _hasher;

        public AdminConfiguration(IPasswordHasher hasher)
        {
            _hasher = hasher;
        }


        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admin");
            ConfigureCommon(builder);
            builder.HasBaseType<User>();
            builder.HasData(
                new Admin 
                {
                    Id=1, 
                    Username = "admin", 
                    PasswordHash = _hasher.HashPassword("admin"),
                    Email = "nikolaanicic99@gmail.com",
                    RoleId=1,
                    DateOfBirth = new System.DateTime(1999,12,7),
                    LastName = "Anicic",
                    Name = "Nikola",
                    Address = "Korenita, Josifa Tronosca 25",
                    ImagePath = "nema slike za sada"
                });
        }
    }
}
