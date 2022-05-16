using Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.ModelConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.Details)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(p => p.Name).IsRequired()
                .HasMaxLength(60);

            builder.HasMany(p => p.Orders)
                .WithMany(o => o.Produce);
        }
    }
}
