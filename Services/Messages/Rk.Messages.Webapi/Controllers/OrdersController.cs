using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Domain.Enums;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrdersNS.Commands.CreateOrder;
using Rk.Messages.Logic.OrdersNS.Commands.SetStatus;
using Rk.Messages.Logic.OrdersNS.Dto;
using Rk.Messages.Logic.OrdersNS.Queries.GetOrder;
using Rk.Messages.Logic.OrdersNS.Queries.GetOrders;
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
        public async Task<long[]> CreateOrders()
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

        /// <summary>Установить статус</summary>
        [HttpPatch("{id:long}/status")]
        public async Task SetStatus(long id, [FromBody] OrderStatus status)
        {
            await _mediator.Send(new SetStatusCommand { OrderId = id, Status = status });
        }

    }
}
