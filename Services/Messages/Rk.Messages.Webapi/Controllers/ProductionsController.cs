using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Domain.Enums;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Commands.DeleteProduct;
using Rk.Messages.Logic.ProductsNS.Commands.SetStatus;
using Rk.Messages.Logic.ProductsNS.Dto;
using Rk.Messages.Logic.ProductsNS.Queries.GetProductsQuery;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    /// <summary>
    /// Управление продукцией, услугами, технологиями
    /// </summary>

    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductionsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>Получить список продукции с отбором и пагинацией </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<PagedResponse<ProductShortDto>> GetProductions([FromQuery] FilterProductsRequest request)
        {
            var result = await _mediator.Send(new GetProductionsQuery { Request = request });
            return result;
        }

        /// <summary>Физически удалить продукцию</summary>
        [HttpDelete("{id:long}")]
        public async Task DeleteProductById(long id)
        {
            await _mediator.Send(new DeleteProductCommand { ProductId = id });
        }

        /// <summary>Установить статус</summary>
        [HttpPatch("{id:long}/status")]
        public async Task SetStatus(long id, [FromBody] ProductStatus status)
        {
            await _mediator.Send(new SetStatusCommand { ProductId = id, Status = status });
        }

        /// <summary>Получить информацию об атрибутах всей продукции</summary>
        [HttpGet("attributes")]
        [AllowAnonymous]
        public async Task<IReadOnlyCollection<AttributeDto>> GetProductAttributes()
        {
            var result = await _mediator.Send(new GetProductAttributesQuery { });
            return result;
        }
    }
}
