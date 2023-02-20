using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Infrastructure.EFCore.Configurations
{
    public class BaseProductConfiguration : IEntityTypeConfiguration<BaseProduct>
    {
        public void Configure(EntityTypeBuilder<BaseProduct> builder)
        {
            builder.HasDiscriminator<int>("ItemType")
                  .HasValue<Product>(1)
                  .HasValue<ServiceProduct>(2)
                  .HasValue<WorkProduct>(3);


            builder.HasMany(self => self.AttributeValues)
            .WithOne(self => self.BaseProduct)
            .HasForeignKey(self => self.BaseProductId);


            builder.HasMany(self => self.ProductDocuments)
           .WithOne(self => self.BaseProduct)
           .HasForeignKey(self => self.BaseProductId);

            builder.HasMany(self => self.Reviews)
          .WithOne(self => self.BaseProduct)
          .HasForeignKey(self => self.BaseProductId);


            builder.HasOne(self => self.Organization)
              .WithMany()
              .HasForeignKey(self => self.OrganizationId);

        }
    }
}
