using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rk.FileStore.Domain;
using Rk.FileStore.Interfaces.Interfaces;
using Rk.FileStore.Webapi.Models;
using Rk.Messages.Common.Exceptions;

namespace Rk.FileStore.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]    
    public class FilesController : ControllerBase
    {
        private readonly IRepository<FileLink> _linksRepository;

        private readonly IRepository<FileData> _dataRepository;

        
        public FilesController(IRepository<FileLink> linksRepository, IRepository<FileData> dataRepository)
        {
            _linksRepository = linksRepository;
            _dataRepository = dataRepository;
        }

        [HttpPost]
        public async Task<Guid> CreateFile([FromBody] CreateFileRequest request)
        {
            Guid result;

            string keySha256 = null;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(request.Data);

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                keySha256 =  builder.ToString();
            }


            var query = _linksRepository.GetAll(x => x.Sha256Key == keySha256);

            if (query.Any())
            {
                var firstLink = await query.FirstAsync();

                result = firstLink.GlobalId;
            }
            else {

                var fileLink = new FileLink(request.FileName, keySha256);

                await _linksRepository.AddAsync(fileLink);

                await _dataRepository.AddAsync(new FileData(keySha256, request.Data));

                await _dataRepository.SaveChangesAsync();

                result = fileLink.GlobalId;
            }

            return result;

        }

        /// <summary>Получить содержимое файла </summary>
        [HttpGet("{globalId}")]
        public async Task<byte[]> GetFileContent(Guid globalId)
        {

            var query = _linksRepository.GetAll(x => x.GlobalId == globalId);

            if (!query.Any())  throw new EntityNotFoundException($"Не найден filelink по id={globalId}");

            var firstLink = await query.FirstAsync();

            var sha256Key = firstLink.Sha256Key;

            if (sha256Key is null) throw new NullReferenceException(nameof(sha256Key));

             var queryData = _dataRepository.GetAll(x => x.Sha256Key == sha256Key);               

            if (!queryData.Any()) throw new NullReferenceException($"Не найден file по sha256={sha256Key}");

            var firstData =await queryData.FirstAsync();

            return firstData.Data;

        }                    
    }
}
