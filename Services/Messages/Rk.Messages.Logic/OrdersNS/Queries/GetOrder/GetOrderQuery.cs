using MediatR;
using Rk.Messages.Logic.OrdersNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Queries.GetOrder
{
    /// <summary>
    /// Получить заказ по Id
    /// </summary>
    public class GetOrderQuery :IRequest<OrderResponse>
    {
        public long OrderId { get; set; }
    }
}
