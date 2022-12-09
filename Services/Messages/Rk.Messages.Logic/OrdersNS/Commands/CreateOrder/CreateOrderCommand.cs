using MediatR;

namespace Rk.Messages.Logic.OrdersNS.Commands.CreateOrder
{
    /// <summary>
    /// Создание заказа
    /// </summary>
    public class CreateOrderCommand : IRequest<long[]>
    {
    }
}
