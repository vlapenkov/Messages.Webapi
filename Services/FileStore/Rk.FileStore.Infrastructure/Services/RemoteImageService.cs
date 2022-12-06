using Rk.FileStore.Interfaces.Dto;
using Rk.FileStore.Interfaces.Services;

namespace Rk.FileStore.Infrastructure.Services
{
    /// <inheritdoc cref="IRemoteImageService"/>
    public class RemoteImageService : IRemoteImageService
    {
        public async Task<FileDataDto> GetFile(Uri requestUri)
        {

            using (var client = new HttpClient())            {

                using (var result = await client.GetAsync(requestUri))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        FileDataDto resultFile = new()
                        {
                            Data = await result.Content.ReadAsByteArrayAsync(),
                            FileName = requestUri.Segments.Last(),
                          //  FileId = Guid.NewGuid()
                        };
                        return resultFile;
                    }
                }
            }

            return null;

        }
    }
}
