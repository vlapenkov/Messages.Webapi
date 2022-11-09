using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrdersNS.Commands.CreateOrder;
using Rk.Messages.Logic.OrdersNS.Dto;
using Rk.Messages.Logic.OrdersNS.Queries.GetOrder;
using Rk.Messages.Logic.OrdersNS.Queries.GetOrders;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;
using System.Threading.Tasks;

namespace Rk.Messages.Webapi.Controllers
{
    /// <summary>
    /// Работа с заказами
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]        
        public async Task<long> CreateOrder()
        {
            return await _mediator.Send(new CreateOrderCommand { });
        }

        [HttpGet("{orderId:long}")]
        public async Task<OrderResponse> GetOrder(long orderId)
        {
            return await _mediator.Send(new GetOrderQuery {OrderId = orderId });
        }

        [HttpGet]
        public async Task<PagedResponse<OrderShortDto>> GetOrders([FromQuery] FilterOrdersRequest request)
        {

            var result = await _mediator.Send(new GetOrdersQuery {Request = request});

            return result;
        }

    }
}
