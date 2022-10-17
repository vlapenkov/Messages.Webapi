using Messages.Domain.Models;
using Messages.Domain.Models.Products;
using Messages.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Messages.Infrastructure.EFCore
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ProductAttribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }

        public DbSet<CatalogSection> CatalogSections { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }
        public DbSet<Technology> TechnologyProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.Entity<BaseProduct>(entity =>
            {
                entity.HasDiscriminator<int>("ItemType")
                  .HasValue<Product>(1)
                  .HasValue<ServiceProduct>(2)
                  .HasValue<Technology>(3);

                entity.HasMany(self => self.AttributeValues)
                .WithOne(self => self.BaseProduct)
                .HasForeignKey(self => self.BaseProductId);

            });

            builder.Entity<CatalogSection>(entity =>
            {
                entity.HasOne(self => self.Parent)
                    .WithMany(self => self.Children)
                    .HasForeignKey(self => self.ParentCatalogSectionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(self => self.Products)
                   .WithOne(self => self.CatalogSection)
                   .HasForeignKey(self => self.CatalogSectionId);
            });

            builder.Entity<AttributeValue>(entity =>
            {
                entity.HasOne(self => self.Attribute)
                    .WithMany()
                    .HasForeignKey(self => self.AttributeId);

            });

            Seed(builder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasData(
          new Organization(1, "Прогресс", "Ракетно-космический центр «Прогресс», Самара", "1146312005344", "6312139922", "631201001"),
          new Organization(2,  "Златоустовский машиностроительный завод", "АКЦИОНЕРНОЕ ОБЩЕСТВО \"ЗЛАТОУСТОВСКИЙ МАШИНОСТРОИТЕЛЬНЫЙ ЗАВОД\"", "1146312005344", "7404052938", "631201001")
          );

            modelBuilder.Entity<ProductAttribute>().HasData(
        new ProductAttribute(1,"Вес"),
        new ProductAttribute(2, "Длина"),
        new ProductAttribute(3, "Ширина"),
        new ProductAttribute(4, "Цвет")
        );

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
