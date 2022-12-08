using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkProductsController
    {

        private readonly IWorkProductsService _productsService;

        private readonly IFileStoreService _filesService;
                

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
    }
}
