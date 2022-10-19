using Messages.Domain.Models.Products;
using Messages.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Messages.Interfaces.Interfaces.DAL
{
    public interface IAppDbContext
    {
        DbSet<Organization> Organizations { get; }
        DbSet<ProductAttribute> Attributes { get; }
        DbSet<AttributeValue> AttributeValues { get; }
        DbSet<CatalogSection> CatalogSections { get; }

        DbSet<Product> Products { get; }
        DbSet<ServiceProduct> ServiceProducts { get; }
        DbSet<Technology> TechnologyProducts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
