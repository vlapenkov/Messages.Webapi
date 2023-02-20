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
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>    {
       
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(self => self.Product)
                .WithMany()
                .HasForeignKey(self => self.ProductId);
        }
    }
}
