using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rk.Messages.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Infrastructure.EFCore.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(self => self.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(self => self.OrderId);

            builder.HasOne(self => self.Organization)
               .WithMany()
               .HasForeignKey(self => self.OrganizationId);

            builder.HasOne(self => self.Producer)
             .WithMany()
             .HasForeignKey(self => self.ProducerId);
        }
    }
}
