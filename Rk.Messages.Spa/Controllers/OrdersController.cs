﻿using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
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
        /// Создать заказы
        /// </summary>        
        [HttpPost]
        public async Task<long[]> CreateOrder()
        {
            return await _service.CreateOrders();
        }

        /// <summary>
        /// Получить заказ
        /// </summary>        
        [HttpGet("{orderId:long}")]
        public async Task<OrderResponse> GetOrder(long orderId)
        {
            return await _service.GetOrder(orderId);
        }

        /// <summary>
        /// Получить список заказов
        /// </summary>     
        [HttpGet]
        public async Task<PagedResponse<OrderShortDto>> GetOrders([FromQuery] FilterOrdersRequest request)
        {

            var result = await _service.GetOrders(request);

            return result;
        }

        /// <summary>Установить статус</summary>
        [HttpPatch("{id:long}/status")]
        public async Task SetStatus(long id, [FromBody] long status)
        {
            await _service.SetStatus(id, status);
        }

    }
}
