using MediatR;
using Rk.Messages.Logic.ServiceProductsNS.Dto;

namespace Rk.Messages.Logic.ServiceProductsNS.Commands.UpdateServiceProduct
{
    /// <summary>
    /// Апдейт услуги
    /// </summary>
    public class UpdateServiceProductCommand : IRequest
    {
        public long ProductId { get; set; }

        public UpdateServiceProductRequest Request { get; set; }
    }
}
