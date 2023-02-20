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
    internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
           builder.HasIndex(self => self.Ogrn).IsUnique();

           builder.HasMany(self => self.OrganizationDocuments)
                   .WithOne(self => self.Organization)
                   .HasForeignKey(self => self.OrganizationId);           
        }
    }
}
