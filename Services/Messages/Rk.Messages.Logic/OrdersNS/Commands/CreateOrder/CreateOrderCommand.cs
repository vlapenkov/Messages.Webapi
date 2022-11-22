using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Commands.CreateOrder
{
    /// <summary>
    /// Создание заказа
    /// </summary>
    public class CreateOrderCommand : IRequest<long>
    {
    }
}
