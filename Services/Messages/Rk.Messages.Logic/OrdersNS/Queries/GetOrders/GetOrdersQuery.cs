using MediatR;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrdersNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Queries.GetOrders
{
    /// <summary>
    /// Запрос получить все заказы
    /// </summary>
    public class GetOrdersQuery : IRequest<PagedResponse<OrderShortDto>>
    {
        public FilterOrdersRequest Request { get; set; }
    }
}
