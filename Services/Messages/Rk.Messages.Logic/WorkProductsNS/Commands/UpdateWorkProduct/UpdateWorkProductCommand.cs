using MediatR;
using Rk.Messages.Logic.WorkProductsNS.Dto;

namespace Rk.Messages.Logic.WorkProductsNS.Commands.UpdateWorkProduct
{
    /// <summary>
    /// Апдейт работы
    /// </summary>
    public class UpdateWorkProductCommand : IRequest
    {
        public long ProductId { get; set; }

        public UpdateWorkProductRequest Request { get; set; }
    }
}
