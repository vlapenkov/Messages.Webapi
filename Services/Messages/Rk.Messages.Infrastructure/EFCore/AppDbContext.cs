using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Domain.Enums;
using Rk.Messages.Infrastructure.EFCore.Configurations;
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

        public DbSet<OrganizationDocument> OrganizationDocuments { get; set; }

        public DbSet<CatalogSection> CatalogSections { get; set; }

        public DbSet<BaseProduct> BaseProduct { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }
        public DbSet<WorkProduct> WorkProducts { get; set; }

        public DbSet<SectionDocument> SectionDocuments { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<ProductsExchange> ProductsExchanges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BaseProductConfiguration())
            .ApplyConfiguration(new CatalogSectionConfiguration())
            .ApplyConfiguration(new ProductDocumentConfiguration())
            .ApplyConfiguration(new SectionDocumentConfiguration())
            .ApplyConfiguration(new AttributeValueConfiguration())
            .ApplyConfiguration(new OrderConfiguration())
            .ApplyConfiguration(new OrderItemConfiguration())
            .ApplyConfiguration(new OrganizationConfiguration())
            .ApplyConfiguration(new OrganizationDocumentConfiguration())
            .ApplyConfiguration(new ShoppingCartItemConfiguration());
                       
            Seed(builder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>().HasData(
          new Organization(1, "Прогресс", "Ракетно-космический центр «Прогресс», Самара", "1146312005344", "6312139922", "631201001", "Самарская область", "Самара", "Самарская обл., г. Самара, ул. Земеца, д. 18", null, null, null, null, null, OrganizationStatus.Working),
          new Organization(2, "Златоустовский машиностроительный завод", "АКЦИОНЕРНОЕ ОБЩЕСТВО \"ЗЛАТОУСТОВСКИЙ МАШИНОСТРОИТЕЛЬНЫЙ ЗАВОД\"", "1146312005355", "7404052938", "631201001", "Челябинская область", "Златоуст", "456227, Челябинская область, город Златоуст, Парковый проезд, 1", "http://www.zlatmash.ru/", null, null, null, null, OrganizationStatus.Working)
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
