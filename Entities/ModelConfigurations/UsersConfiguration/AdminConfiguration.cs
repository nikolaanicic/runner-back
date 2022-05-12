using Entities.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Entities.ModelConfigurations.UsersConfiguration
{
    public class AdminConfiguration : BaseUserConfiguration<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admin");
            ConfigureCommon(builder);
        }
    }
}
