using Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Property(o => o.TotalPrice).IsRequired();
            builder.Property(o => o.DeliveryTimer).IsRequired();
            builder.Property(o => o.Address).IsRequired();


            builder.HasOne(o => o.Consumer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ConsumerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Deliverer)
                .WithMany(d => d.Orders)
                .HasForeignKey(o=>o.DelivererId)
                .OnDelete(DeleteBehavior.Cascade);
        
        }
    }
}
