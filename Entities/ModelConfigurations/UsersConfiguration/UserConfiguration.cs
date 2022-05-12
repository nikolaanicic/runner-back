using Entities.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Entities.ModelConfigurations.UsersConfiguration
{
    public class UserConfiguration : BaseUserConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            ConfigureCommon(builder);
        }
    }
}
