using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;

namespace Rk.Messages.Interfaces.Interfaces.DAL
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
