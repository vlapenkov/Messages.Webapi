using MediatR;
using Rk.Messages.Domain.Enums;

namespace Rk.Messages.Logic.ProductsNS.Commands.AddExchange
{
    /// <summary>
    /// Регистрация обмена товарами
    /// </summary>
    public class RegisterProductExchangeCommand : IRequest
    {
        public ProductExchangeType ExchangeType { get; set; }

        public long ProductsLoaded { get; set; }
    }
}
