using Messages.Domain;
using Microsoft.EntityFrameworkCore;

namespace Messages.Infrastructure
{
    public interface ITransientDbContext
    {
        public DbSet<Message> Messages { get; set; }
        int SaveChanges();
    }
}