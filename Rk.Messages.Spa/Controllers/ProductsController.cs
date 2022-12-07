using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.FileStoreNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Управление продукцией, услугами, технологиями
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]    
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        private readonly IFileStoreService _filesService;

        private readonly IProductsPrepareService _productsPrepareService;

        private readonly ILogger _logger;


        public ProductsController(IProductsService productsService, IFileStoreService filesService, IProductsPrepareService productsPrepareService, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _filesService = filesService;
            _productsPrepareService = productsPrepareService;
            _logger = logger;   
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

        /// <summary>Создать продукты из excel </summary>
        [HttpPost("fromexcel")]
        public async Task<IReadOnlyCollection<CreateProductRequest>> CreateProducts([FromBody] CreateProductsFromFileRequest request)
        {
            IReadOnlyCollection<CreateProductRequest> productsPrepared = await _productsPrepareService.PrepareProductsFromExcel(request);

            foreach (var productRequest in productsPrepared)
            {
                var productId = await _productsService.CreateProduct(productRequest);

                _logger.LogInformation($"Создана продукция id={productId}");
            }

            return productsPrepared;

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
            byte[][] resultFiles = Array.Empty<byte[]>();

            var product =  await _productsService.GetProduct(id);

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

        /// <summary>Апдейт значений атрибутов товара</summary>
        [HttpPut("{id:long}/attributes")]
        public async Task UpdateAttributes(long id, [FromBody] IReadOnlyCollection<AttributeValueDto> attributeValues)
        {
            await _productsService.UpdateAttributes(id, attributeValues);
        }

        /// <summary>Получить информацию об атрибутах всей продукции</summary>
        [HttpGet("attributes")]
        public async Task<IReadOnlyCollection<AttributeDto>> GetProductAttributes()
        {
            var result = await _productsService.GetProductAttributes();

            return result;
        }

        /// <summary>Удалить продукцию</summary>
        [HttpDelete("{id:long}")]
        public async Task DeleteProductById(long id)
        {
            await _productsService.DeleteProductById(id);
        }

        /// <summary>Установить статус</summary>
        [HttpPatch("{id:long}/status")]
        public async Task SetStatus(long id, [FromBody] long status)
        {
            await _productsService.SetStatus(id, status);
        }
    }
}
