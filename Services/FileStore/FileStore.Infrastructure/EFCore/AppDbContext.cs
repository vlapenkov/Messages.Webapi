using FileStore.Domain;
using FileStore.Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FileStore.Infrastructure.EFCore
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}


        public DbSet<FileData> FileData { get; set; }
        public DbSet<FileLink> FileLinks { get; set; }        

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.Entity<CatalogSection>(entity =>
            //{
            //    entity.HasOne(self => self.Parent)
            //        .WithMany(self => self.Children)
            //        .HasForeignKey(self => self.ParentCatalogSectionId)
            //        .OnDelete(DeleteBehavior.Cascade);

            //    entity.HasMany(self => self.Products)
            //       .WithOne(self => self.CatalogSection)
            //       .HasForeignKey(self => self.CatalogSectionId);
            //});

            //builder.Entity<AttributeValue>(entity =>
            //{
            //    entity.HasOne(self => self.Attribute)
            //        .WithMany()
            //        .HasForeignKey(self => self.AttributeId);

            //});

            Seed(builder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder)
        {
        
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
