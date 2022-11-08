using Microsoft.AspNetCore.Mvc;
using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.OrdersNS;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IOrdersService
    {
        [Post("/api/v1/Orders")]
        Task<long> CreateOrder();

        [Get("/api/v1/Orders/{orderId}")]
        Task<OrderResponse> GetOrder(long orderId);
        
    }
}
