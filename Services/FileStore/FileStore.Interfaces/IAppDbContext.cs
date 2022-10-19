using FileStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<FileData> FileData { get; }
        DbSet<FileLink> FileLinks { get;  }
    }
}