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

        /// <summary>Создать товар</summary>
        [HttpPost]        
        public async Task<long> CreateProduct([FromBody] CreateProductRequest request)
        {

            var fileGlobalIds = await _filesService.CreateFiles(request.Documents.Select(doc => new CreateFileRequest { FileName = doc.FileName, Data = doc.Data }).ToArray());

            var fileGlobalIdsArray = fileGlobalIds.ToArray();

            byte counter = 0;

            request.Documents.ForEach(doc => doc.FileId = fileGlobalIdsArray[counter++]);

            return await _productsService.CreateProduct(request);

        }

        /// <summary>Создать товары из excel </summary>
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

        /// <summary>Получить информацию о товаре</summary>
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

        /// <summary>Апдейт товара</summary>
        [HttpPut("{id:long}")]
        public async Task UpdateProduct(long id, [FromBody] UpdateProductRequest request)
        {
            await _productsService.UpdateProduct(id, request);
        }



    }
}
