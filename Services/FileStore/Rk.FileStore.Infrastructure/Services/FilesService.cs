using Microsoft.EntityFrameworkCore;
using Rk.FileStore.Domain;
using Rk.FileStore.Interfaces;
using Rk.FileStore.Interfaces.Dto;
using Rk.FileStore.Interfaces.Interfaces;
using Rk.FileStore.Interfaces.Services;

namespace Rk.FileStore.Infrastructure.Services
{
    /// <inheritdoc cref="IFilesService"/>
    public class FilesService : IFilesService
    {
        private readonly IRepository<FileLink> _linksRepository;

        private readonly IRepository<FileData> _dataRepository;

        private readonly IHashProvider _hashProvider;

        public FilesService(IRepository<FileLink> linksRepository, IRepository<FileData> dataRepository, IHashProvider hashProvider)
        {
            _linksRepository = linksRepository;
            _dataRepository = dataRepository;
            _hashProvider = hashProvider;
        }

        public async Task<Guid> CreateFile(IFileData request)
        {

            Guid result;

            string keySha256 = _hashProvider.GetHash(request.Data);

            var query = _linksRepository.GetAll(x => x.Sha256Key == keySha256);

            if (query.Any())
            {
                var firstLink = await query.FirstAsync();

                result = firstLink.GlobalId;
            }
            else
            {

                var fileLink = new FileLink(request.FileName, keySha256);

                await _linksRepository.AddAsync(fileLink);

                await _dataRepository.AddAsync(new FileData(keySha256, request.Data));

                await _dataRepository.SaveChangesAsync();

                result = fileLink.GlobalId;
            }

            return result;
        }

        public async Task<IReadOnlyCollection<Guid>> CreateFiles(IReadOnlyCollection<IFileData> requests)
        {
            List<Guid> results = new();

            foreach (var request in requests)
            {
                var keySha256 = _hashProvider.GetHash(request.Data);


                var query = _linksRepository.GetAll(x => x.Sha256Key == keySha256);

                if (query.Any())
                {
                    var firstLink = await query.FirstAsync();

                    results.Add(firstLink.GlobalId);
                }
                else
                {

                    var fileLink = new FileLink(request.FileName, keySha256);

                    await _linksRepository.AddAsync(fileLink);

                    await _dataRepository.AddAsync(new FileData(keySha256, request.Data));

                    await _dataRepository.SaveChangesAsync();

                    results.Add(fileLink.GlobalId);
                }
            }

            return results;
        }

        public async Task<byte[]> GetFileContent(Guid globalId)
        {
            var query = _linksRepository.GetAll(x => x.GlobalId == globalId);

            if (!query.Any()) throw new Exception($"Не найден filelink по id={globalId}");

            var firstLink = await query.FirstAsync();

            var sha256Key = firstLink.Sha256Key;

            if (sha256Key is null) throw new NullReferenceException(nameof(sha256Key));

            var queryData = _dataRepository.GetAll(x => x.Sha256Key == sha256Key);

            if (!queryData.Any()) throw new NullReferenceException($"Не найден file по sha256={sha256Key}");

            var firstData = await queryData.FirstAsync();

            return firstData.Data;
        }
    }
}
