using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;

namespace Rk.Messages.Infrastructure.EFCore
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IUserService _userService;
        public AppDbContext(DbContextOptions<AppDbContext> options, IUserService userService) : base(options)
        {
            _userService = userService;           
        }


        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ProductAttribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }

        public DbSet<CatalogSection> CatalogSections { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }
        public DbSet<Technology> TechnologyProducts { get; set; }

        public DbSet<SectionDocument> SectionDocuments { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
                      

            builder.Entity<BaseProduct>(entity =>
            {
                entity.HasDiscriminator<int>("ItemType")
                  .HasValue<Product>(1)
                  .HasValue<ServiceProduct>(2)
                  .HasValue<Technology>(3);

              //  entity.HasIndex(self => self.Name);//.IsUnique();

                
                entity.HasMany(self => self.AttributeValues)
                .WithOne(self => self.BaseProduct)
                .HasForeignKey(self => self.BaseProductId);


                entity.HasMany(self => self.ProductDocuments)
               .WithOne(self => self.BaseProduct)
               .HasForeignKey(self => self.BaseProductId);


                entity.HasOne(self => self.Organization)
                  .WithMany()
                  .HasForeignKey(self => self.OrganizationId);


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

                entity.HasMany(self => self.SectionDocuments)
                  .WithOne(self => self.Section)
                  .HasForeignKey(self => self.CatalogSectionId);
            });

            builder.Entity<AttributeValue>(entity =>
            {
                entity.HasOne(self => self.Attribute)
                    .WithMany()
                    .HasForeignKey(self => self.AttributeId);

            });

            builder.Entity<ProductDocument>(entity =>
            {
                entity.HasOne(self => self.Document)
                    .WithMany()
                    .HasForeignKey(self => self.DocumentId);

            });

            builder.Entity<SectionDocument>(entity =>
            {
                entity.HasOne(self => self.Document)
                    .WithMany()
                    .HasForeignKey(self => self.DocumentId);

                //entity.HasOne(self => self.Section)
                //   .WithMany()
                //   .HasForeignKey(self => self.CatalogSectionId);

            });

            builder.Entity<Order>(entity =>
            {
                entity.HasMany(self => self.OrderItems)
                    .WithOne(oi=>oi.Order)
                    .HasForeignKey(self => self.OrderId);

                entity.HasOne(self => self.Organization)
                   .WithMany()
                   .HasForeignKey(self => self.OrganizationId);

            });

            builder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(self => self.Product)
                    .WithMany()
                    .HasForeignKey(self => self.ProductId);

            });

            builder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasOne(self => self.Product)
                    .WithMany()
                    .HasForeignKey(self => self.ProductId);

            });

            // Ограничение на ОГРН  - должен быть уникальным

            builder.Entity<Organization>(entity =>
            {
                entity.HasIndex(self => self.Ogrn).IsUnique();
            });


            Seed(builder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasData(
          new Organization(1, "Прогресс", "Ракетно-космический центр «Прогресс», Самара", "1146312005344", "6312139922", "631201001", "Самарская область","Самара", "Самарская обл., г. Самара, ул. Земеца, д. 18",null,null,null, OrganizationStatus.Working),
          new Organization(2,  "Златоустовский машиностроительный завод", "АКЦИОНЕРНОЕ ОБЩЕСТВО \"ЗЛАТОУСТОВСКИЙ МАШИНОСТРОИТЕЛЬНЫЙ ЗАВОД\"", "1146312005344", "7404052938", "631201001","Челябинская область","Златоуст", "456227, Челябинская область, город Златоуст, Парковый проезд, 1", "http://www.zlatmash.ru/", null, null, OrganizationStatus.Working)
          );

            modelBuilder.Entity<ProductAttribute>().HasData(
                new ProductAttribute(1,"Масса, кг"),
                new ProductAttribute(2, "Длина, м"),
                new ProductAttribute(3, "Ширина, м "),
                new ProductAttribute(4, "Цвет"),
                new ProductAttribute(5, "Объем, куб. м"),
                new ProductAttribute(6, "Диапазон измерений,  Дб"),
                new ProductAttribute(7, "Частотный диапазон,  Гц"),
                new ProductAttribute(8, "Срок службы,  лет"),
                new ProductAttribute(9, "Погрешность, не более,  Дб"),
                new ProductAttribute(10, "Напряжение питания постоянного тока,  В"),
                new ProductAttribute(11, "Ток потребления, не более,  мА")
        );

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities(ChangeTracker.Entries<AuditableEntity>());

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities(IEnumerable<EntityEntry<AuditableEntity>> entities)
        {
            foreach (var entity in entities)

                switch (entity.State)
                {
                    case EntityState.Added:
                        {
                            entity.Entity.SetCreated(_userService.UserName);
                            break;
                        }
                        case EntityState.Modified:
                        {
                            entity.Entity.SetLastModified(_userService.UserName);
                            break;
                        }                        
                }
        }
    }
}
