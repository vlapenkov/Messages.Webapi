using Microsoft.AspNetCore.Authorization;
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
        private readonly IProductionsService _service;

        public ProductionsController(IProductionsService service)
        {
            _service = service;
        }

        /// <summary>Получить список товаров, услуг, продукции с отбором и пагинацией </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<PagedResponse<ProductShortDto>> GetProducts([FromQuery] FilterProductsRequest request)
        {
            return await _service.GetProducts(request);
        }

        /// <summary>Удалить продукцию</summary>
        [HttpDelete("{id:long}")]
        public async Task DeleteProductById(long id)
        {
            await _service.DeleteProductById(id);
        }

        /// <summary>Установить статус</summary>
        [HttpPatch("{id:long}/status")]
        public async Task SetStatus(long id, [FromBody] long status)
        {
            await _service.SetStatus(id, status);
        }

        /// <summary>Получить информацию об атрибутах всей продукции</summary>
        [AllowAnonymous]
        [HttpGet("attributes")]
        public async Task<IReadOnlyCollection<AttributeDto>> GetProductAttributes()
        {
            var result = await _service.GetProductAttributes();

            return result;
        }

        /// <summary>Добавить отзыв о продукции</summary>  
        [HttpPost("{id:long}/reviews")]
        public async Task AddReview(long id, [FromBody] CreateReviewRequest request)
        {
            await _service.AddReview(id, request);
        }
    }
}
