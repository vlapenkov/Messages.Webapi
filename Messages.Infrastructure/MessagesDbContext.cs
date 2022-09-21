using Messages.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Infrastructure
{
    public class MessagesDbContext : DbContext, ITransientDbContext
    {
        public MessagesDbContext(DbContextOptions<MessagesDbContext> options) : base(options)
        {
        }


        public DbSet<Message> Messages { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity => entity.Property(p => p.Created).HasDefaultValueSql("getdate()"));

            Seed(modelBuilder);
        }

        protected virtual void Seed(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Message>().HasData(
          new Message {Id = 1,SenderId = 1, ReceiverId=2,Name="Name", Description= "Description" },
          new Message { Id = 2, SenderId = 2, ReceiverId = 1, Name = "Name", Description = "Description" },
          new Message { Id = 3, SenderId = 1, ReceiverId = 3, Name = "Name", Description = "Description" },
          new Message { Id = 4, SenderId = 3, ReceiverId = 1, Name = "Name", Description = "Description" },
          new Message { Id = 5, SenderId = 2, ReceiverId = 3, Name = "Name", Description = "Description" },
          new Message { Id = 6, SenderId = 3, ReceiverId = 2, Name = "Name", Description = "Description" }
          );

        }
        
    }
}
