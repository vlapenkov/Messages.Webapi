using Microsoft.AspNetCore.Mvc;
using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.OrdersNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IOrdersService
    {
        /// <summary>
        /// Создать заказ
        /// </summary>
        /// <returns>Номер заказа</returns>
        [Post("/api/v1/Orders")]
        Task<long> CreateOrder();

        /// <summary>
        /// Получить заказ
        /// </summary>
        /// <param name="orderId">Id заказа</param>
        /// <returns></returns>
        [Get("/api/v1/Orders/{orderId}")]
        Task<OrderResponse> GetOrder(long orderId);

        /// <summary>
        /// Получить пагинированный список заказов
        /// </summary>
        /// <param name="request">фильтр</param>
        /// <returns>пагинированный список заказов</returns>
        [Get("/api/v1/Orders")]
        Task<PagedResponse<OrderShortDto>> GetOrders([Query] FilterOrdersRequest request);
    }
}
