using MediatR;
using Rk.Messages.Domain.Enums;

namespace Rk.Messages.Logic.ProductsNS.Commands.SetStatus
{
    /// <summary>
    /// Изменить статус продукции
    /// </summary>
    public class SetStatusCommand : IRequest
    {
        public long ProductId { get; set; }

        public ProductStatus Status { get; set; }
    }
}
