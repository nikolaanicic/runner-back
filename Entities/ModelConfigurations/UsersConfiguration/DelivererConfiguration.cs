using Entities.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.ModelConfigurations.UsersConfiguration
{
    public class DelivererConfiguration : BaseUserConfiguration<Deliverer>
    {
        public override void Configure(EntityTypeBuilder<Deliverer> builder)
        {
            builder.ToTable("Deliverer");
            ConfigureCommon(builder);
        }
    }
}
