using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Rk.Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController :ControllerBase
    {
        private readonly IMediator _mediatr;

        public ProductsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Создать продукцию
        /// </summary>
        /// <param name="request">запрос создания</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> CreateProduct([FromBody] CreateProductRequest request)
        {

            return await _mediatr.Send(new CreateProductCommand { Request = request });
        }

        /// <summary>Получить список продукции с отбором и пагинацией </summary>
        [HttpGet]
        public async Task<PagedResponse<ProductShortDto>> GetProducts([FromQuery] FilterProductsRequest request)
        {

            var result = await _mediatr.Send(new GetProductsQuery {Request = request });

            return result;
        }

        /// <summary>Получить информацию о продукции</summary>
        [HttpGet("{id:long}")]
        public async Task<ProductResponse> GetProduct(long id)
        {

            var result = await _mediatr.Send(new GetProductQuery { Id=id });

            return result;
        }
    }
}
