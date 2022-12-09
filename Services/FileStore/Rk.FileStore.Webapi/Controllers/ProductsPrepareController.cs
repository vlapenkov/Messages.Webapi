using Microsoft.AspNetCore.Mvc;
using Rk.FileStore.Interfaces;
using Rk.FileStore.Interfaces.Dto;
using Rk.FileStore.Interfaces.Services;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.FileStore.Webapi.Controllers
{
    /// <summary>
    /// Подготовить данные для продукции из excel
    /// </summary>

    [Route("api/v1/[controller]")]
    [ApiController]    
    public class ProductsPrepareController : ControllerBase
    {
        private readonly IFilesService _filesService;

        private readonly IProductService _productService;

        public ProductsPrepareController(IFilesService filesService, IProductService productService)
        {
            _filesService = filesService;
            _productService = productService;
        }


        /// <summary>
        /// Создать продукцию из excel-файла, передаваемого через base64
        /// </summary>        
        /// 
        [HttpPost]
        public async Task<IReadOnlyCollection<ProductDto>> PrepareProductsFromExcel([FromBody] CreateProductsFromFileRequest request)
        {
            var documentId = await _filesService.CreateFile(request);

            var contents = await _filesService.GetFileContent(documentId);

            var products = await _productService.GenerateProductData(contents);

            foreach (var product in products)
            {
                if (product.Documents.Any())
                {
                    FileDataDto firstDocument = product.Documents.First();

                    var fileId = await _filesService.CreateFile(firstDocument);

                    firstDocument.FileId = fileId;
                }
            }

            return products;

        }
    }
}
