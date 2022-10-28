using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Работа с продуктами
    /// </summary>
    [Route( "api/[controller]" )]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        private readonly IFileStoreService _fileService;
                

        public ProductsController(IProductsService productsService, IFileStoreService fileService)
        {
            _productsService = productsService;
            _fileService = fileService;
        }

        /// <summary>Создать продукт </summary>
        [HttpPost]
        //[Authorize]
        public async Task<long> CreateProduct([FromBody] CreateProductRequest request)
        {
            var tasks = new Task<Guid>[request.Documents.Count];


            for (int i = 0; i < request.Documents.Count; i++)
            {
                tasks[i] = _fileService.CreateFile(new CreateFileRequest { FileName = request.Documents[i].FileName,Data = request.Documents[i].Data });
            }
            
             var resultFiles = await Task.WhenAll(tasks);

            int counter = 0;

            request.Documents.ForEach(fileData => fileData.FileId = resultFiles[counter++]);

            return await _productsService.CreateProduct(request);
        }

        /// <summary>Получить список товаров с отбором и пагинацией </summary>
        [HttpGet]
        public async Task<PagedResponse<ProductShortDto>> GetProducts([FromQuery] FilterProductsRequest request)
        {
            return await _productsService.GetProducts(request);
        }

        /// <summary>Получить информацию о продукции</summary>
        [HttpGet("{id:long}")]
        public async Task<ProductResponse> GetProduct(long id)
        {
            return await _productsService.GetProduct(id);
           
        }
    }
}
