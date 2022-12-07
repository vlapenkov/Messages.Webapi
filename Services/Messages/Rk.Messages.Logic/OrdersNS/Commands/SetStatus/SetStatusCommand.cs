using MediatR;
using Rk.Messages.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Commands.SetStatus
{
    /// <summary>
    /// Изменить статус заказа
    /// </summary>
    public class SetStatusCommand :IRequest
    {
        public long OrderId { get; set; }

        public OrderStatus Status { get; set; }
    }
}
