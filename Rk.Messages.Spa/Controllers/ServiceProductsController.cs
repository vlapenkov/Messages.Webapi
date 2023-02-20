using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.ServiceProductNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Управление услугами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProductsController : ControllerBase
    {
        private readonly IServiceProductsService _productsService;

        private readonly IFileStoreService _filesService;
                

        /// <inheritdoc />
        public ServiceProductsController(IServiceProductsService productsService, IFileStoreService filesService)
        {
            _productsService = productsService;

            _filesService = filesService;
        }

        /// <summary>Создать услугу</summary>
        [HttpPost]
        public async Task<long> CreateServiceProduct([FromBody] CreateServiceProductRequest request)
        {

            var fileGlobalIds = await _filesService.CreateFiles(request.Documents.Select(doc => new CreateFileRequest { FileName = doc.FileName, Data = doc.Data }).ToArray());

            var fileGlobalIdsArray = fileGlobalIds.ToArray();

            byte counter = 0;

            request.Documents.ForEach(doc => doc.FileId = fileGlobalIdsArray[counter++]);

            return await _productsService.CreateServiceProduct(request);

        }

        /// <summary>Получить информацию о услуге</summary>
        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<ServiceProductResponse> GetServiceProduct(long id)
        {
            byte[][] resultFiles = Array.Empty<byte[]>();

            var product = await _productsService.GetServiceProduct(id);

            if (product.Documents.Any())
            {
                var tasks = new Task<byte[]>[product.Documents.Count()];

                for (int i = 0; i < product.Documents.Count(); i++)
                {
                    tasks[i] = _filesService.GetFileContent(product.Documents[i].FileId);
                }

                resultFiles = await Task.WhenAll(tasks);

            }

            for (int i = 0; i < resultFiles.Length; i++)

                product.Documents[i].Data = resultFiles[i];

            return product;
        }

        /// <summary>Апдейт услуги</summary>
        [HttpPut("{id:long}")]
        public async Task UpdateServiceProduct(long id, [FromBody] UpdateServiceProductRequest request)
        {
            await _productsService.UpdateServiceProduct(id, request);
        }
    }
}
