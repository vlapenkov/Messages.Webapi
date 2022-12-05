using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Rk.FileStore.Domain;
using Rk.FileStore.Interfaces;
using Rk.FileStore.Interfaces.Interfaces;
using Rk.FileStore.Interfaces.Services;
using Rk.Messages.Common.Exceptions;

namespace Rk.FileStore.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]    
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;

        public FilesController(IFilesService filesService)
        {
            _filesService = filesService;
        }

        [HttpPost]
        public async Task<Guid> CreateFile([FromBody] CreateFileRequest request)
        {
            return await _filesService.CreateFile(request);
        }

        [HttpPost("bulk")]
        public async Task<IReadOnlyCollection<Guid>> CreateFiles([FromBody] IReadOnlyCollection<CreateFileRequest> requests)
        {
            return await _filesService.CreateFiles(requests);

        }

        /// <summary>Получить содержимое файла </summary>
        [HttpGet("{globalId}")]
        public async Task<byte[]> GetFileContent(Guid globalId)
        {
            return await _filesService.GetFileContent(globalId);    
        }                    
    }
}
