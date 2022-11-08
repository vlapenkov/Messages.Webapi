using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.OrdersNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
{
    /// <summary>
    /// Работа с заказами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }

        /// <summary>
        /// Создать заказ
        /// </summary>        
        [HttpPost]
        public async Task<long> CreateOrder()
        {
            return await _service.CreateOrder();
        }

        /// <summary>
        /// Получить заказ
        /// </summary>        
        [HttpGet("{orderId:long}")]
        public async Task<OrderResponse> GetOrder(long orderId)
        {
            return await _service.GetOrder(orderId);
        }

    }
}
