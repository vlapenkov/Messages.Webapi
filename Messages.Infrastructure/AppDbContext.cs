using Messages.Domain.Models;
using Messages.Domain.Models.Products;
using Messages.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messages.Infrastructure
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Organization> Organizations { get; set; }

        public DbSet<ProductAttribute> Attributes { get; set; }

        public DbSet<AttributeValue> AttributeValues { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }
        public DbSet<Technology> TechnologyProducts { get; set; }

              
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.UseLazyLoadingProxies();

            

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

            builder.Entity<CatalogSection>(entity => {
                entity.HasOne(self => self.Parent)
                    .WithMany(self => self.Children)
                    .HasForeignKey(self => self.ParentCatalogSectionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(self => self.Products)
                   .WithOne(self => self.CatalogSection)
                   .HasForeignKey(self => self.CatalogSectionId);                   
            });

            builder.Entity<AttributeValue>(entity => {
                entity.HasOne(self => self.Attribute)
                    .WithMany()
                    .HasForeignKey(self => self.AttributeId);
                    
            });

            Seed(builder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasData(
          new Organization(1,"Прогресс", "Ракетно-космический центр «Прогресс», Самара", "1146312005344", "6312139922", "631201001")          
          );

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
