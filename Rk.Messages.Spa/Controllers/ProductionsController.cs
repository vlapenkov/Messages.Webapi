using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Управление продукцией, услугами, технологиями
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]

    public class ProductionsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductionsController(IProductsService productsService)
        {
            _productsService = productsService;
        }


        /// <summary>Получить список товаров, услуг, продукции с отбором и пагинацией </summary>
        [HttpGet]
        public async Task<PagedResponse<ProductShortDto>> GetProducts([FromQuery] FilterProductsRequest request)
        {
            return await _productsService.GetProducts(request);
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

        /// <summary>Получить информацию об атрибутах всей продукции</summary>
        [HttpGet("attributes")]
        public async Task<IReadOnlyCollection<AttributeDto>> GetProductAttributes()
        {
            var result = await _productsService.GetProductAttributes();

            return result;
        }
    }
}
