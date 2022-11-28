using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Работа с продукцией
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        private readonly IFileStoreService _filesService;


        public ProductsController(IProductsService productsService, IFileStoreService fileService)
        {
            _productsService = productsService;

            _filesService = fileService;
        }

        /// <summary>Создать продукт </summary>
        [HttpPost]        
        public async Task<long> CreateProduct([FromBody] CreateProductRequest request)
        {

            var fileGlobalIds = await _filesService.CreateFiles(request.Documents.Select(doc => new CreateFileRequest { FileName = doc.FileName, Data = doc.Data }).ToArray());

            var fileGlobalIdsArray = fileGlobalIds.ToArray();

            byte counter = 0;

            request.Documents.ForEach(doc => doc.FileId = fileGlobalIdsArray[counter++]);

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

        /// <summary>Получить информацию об атрибутах всей продукции</summary>
        [HttpGet("attributes")]
        public async Task<IReadOnlyCollection<AttributeDto>> GetProductAttributes()
        {
            var result = await _productsService.GetProductAttributes();

            return result;
        }
    }
}
