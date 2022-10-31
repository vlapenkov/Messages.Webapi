using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Rk.Messages.Common.Exceptions;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Services;
using Serilog.Sinks.File;

namespace Rk.Messages.Spa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>
    /// Управление документами
    /// </summary>
    public class FilesController : ControllerBase
    {        
        
        private readonly IFileStoreService _fileService;

        /// <summary>
        /// Конструктор
        /// </summary>        
        public FilesController(IFileStoreService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Создать файл
        /// </summary>        
        [HttpPost]
        public async Task<Guid> CreateFile([FromBody] CreateFileRequest request)
        {
            return await _fileService.CreateFile(request);            

        }

        /// <summary>
        /// Создать файлы
        /// </summary>   
        [HttpPost("bulk")]
        public async Task<IReadOnlyCollection<Guid>> CreateFiles([FromBody] IReadOnlyCollection<CreateFileRequest> requests)
        {
            return await _fileService.CreateFiles(requests);           
        }

        /// <summary>Получить содержимое файла </summary>
        [HttpGet("{globalId}")]
        public async Task<byte[]> GetFileContent(Guid globalId)
        {
            return await _fileService.GetFileContent(globalId);

        }                    
    }
}
