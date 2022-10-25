using Microsoft.EntityFrameworkCore;
using Rk.FileStore.Domain;

namespace Rk.FileStore.Interfaces.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<FileData> FileData { get; }
        DbSet<FileLink> FileLinks { get; }
    }
}