using MediatR;
using Rk.Messages.Logic.ServiceProductsNS.Dto;

namespace Rk.Messages.Logic.ServiceProductsNS.Commands.CreateServiceProduct
{
    public class CreateServiceProductCommand : IRequest<long>
    {
        public CreateServiceProductRequest Request { get; set; }
    }
}
