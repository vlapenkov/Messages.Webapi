using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Interfaces.Interfaces.DAL
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
