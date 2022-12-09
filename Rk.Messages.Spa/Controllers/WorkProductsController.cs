using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Управление работами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkProductsController
    {

        private readonly IWorkProductsService _productsService;

        private readonly IFileStoreService _filesService;


        /// <inheritdoc cref="WorkProductsController" />
        public WorkProductsController(IWorkProductsService productsService, IFileStoreService filesService)
        {
            _productsService = productsService;

            _filesService = filesService;
        }

        /// <summary>Создать работу</summary>
        [HttpPost]
        public async Task<long> CreateWorkProduct([FromBody] CreateWorkProductRequest request)
        {

            var fileGlobalIds = await _filesService.CreateFiles(request.Documents.Select(doc => new CreateFileRequest { FileName = doc.FileName, Data = doc.Data }).ToArray());

            var fileGlobalIdsArray = fileGlobalIds.ToArray();

            byte counter = 0;

            request.Documents.ForEach(doc => doc.FileId = fileGlobalIdsArray[counter++]);

            return await _productsService.CreateWorkProduct(request);

        }

        /// <summary>Получить информацию о продукции</summary>
        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<WorkProductResponse> GetProduct(long id)
        {
            byte[][] resultFiles = Array.Empty<byte[]>();

            var product = await _productsService.GetWorkProduct(id);

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

        /// <summary>Апдейт работы</summary>
        [HttpPut("{id:long}")]
        public async Task UpdateWorkProduct(long id, [FromBody] UpdateWorkProductRequest request)
        {
            await _productsService.UpdateWorkProduct(id, request);
        }
    }
}
