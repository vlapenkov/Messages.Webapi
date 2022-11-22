using Microsoft.AspNetCore.Mvc;
using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IFileStoreService
    {
        [Post("/api/v1/files")]
        Task<Guid> CreateFile([Body] CreateFileRequest request);

        [Post("/api/v1/files/bulk")]
        Task<IReadOnlyCollection<Guid>> CreateFiles([Body] IReadOnlyCollection<CreateFileRequest> requests);

        [Get("/api/v1/files/{globalId}")]
        Task<byte[]> GetFileContent(Guid globalId);
    }
    
}
