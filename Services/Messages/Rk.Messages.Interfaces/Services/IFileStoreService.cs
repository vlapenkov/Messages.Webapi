using Refit;
using Rk.Messages.Interfaces.Contracts;
using System;
using System.Threading.Tasks;

namespace Rk.Messages.Interfaces.Services
{
    public interface IFileStoreService
    {
        [Post("/api/v1/files")]
        Task<Guid> CreateFile([Body] CreateFileRequest request);

        [Get("/api/v1/files/{globalId}")]
        Task<byte[]> GetFileContent(Guid globalId);
    }
    
}
