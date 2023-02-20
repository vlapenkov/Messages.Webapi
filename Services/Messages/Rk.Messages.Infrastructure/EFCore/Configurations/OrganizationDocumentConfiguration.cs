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
    internal class OrganizationDocumentConfiguration : IEntityTypeConfiguration<OrganizationDocument>
    {
        public void Configure(EntityTypeBuilder<OrganizationDocument> builder)
        {
            builder.HasOne(self => self.Document)
                    .WithMany()
                    .HasForeignKey(self => self.DocumentId);
        }
    }
}
