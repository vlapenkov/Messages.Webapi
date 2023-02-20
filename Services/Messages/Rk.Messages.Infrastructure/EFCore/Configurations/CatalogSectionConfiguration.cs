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
    internal class CatalogSectionConfiguration : IEntityTypeConfiguration<CatalogSection>
    {
        public void Configure(EntityTypeBuilder<CatalogSection> builder)
        {
            builder.HasOne(self => self.Parent)
                    .WithMany(self => self.Children)
                    .HasForeignKey(self => self.ParentCatalogSectionId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(self => self.Products)
               .WithOne(self => self.CatalogSection)
               .HasForeignKey(self => self.CatalogSectionId);

            builder.HasMany(self => self.SectionDocuments)
              .WithOne(self => self.Section)
              .HasForeignKey(self => self.CatalogSectionId);

           
        }
    }
}
