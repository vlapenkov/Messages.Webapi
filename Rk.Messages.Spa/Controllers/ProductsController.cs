using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductsService _productsService;


        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>Создать продукт </summary>
        [HttpPost]
        public async Task<long> CreateProduct([FromBody] CreateProductRequest request)
        {

            return await _productsService.CreateProduct(request);
        }

        /// <summary>Получить список товаров с отбором и пагинацией </summary>
        [HttpGet]
        [Authorize("Admin")]
        public async Task<PagedResponse<ProductShortDto>> GetProducts([FromQuery] FilterProductsRequest request)
        {
            return await _productsService.GetProducts(request);
        }

        /// <summary>Получить информацию о продукции</summary>
        [HttpGet("{id:long}")]
        [Authorize("Manager")]
        public async Task<ProductResponse> GetProduct(long id)
        {
            return await _productsService.GetProduct(id);
           
        }
    }
}
