using Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.ModelConfigurations.UsersConfiguration
{
    public class ConsumerConfiguration : BaseUserConfiguration<Consumer>
    {
        public override void Configure(EntityTypeBuilder<Consumer> builder)
        {
            builder.ToTable("Consumer");
            builder.HasBaseType<User>();
            ConfigureCommon(builder);
        }
    }
}
