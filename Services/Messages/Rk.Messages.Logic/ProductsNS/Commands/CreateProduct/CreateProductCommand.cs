using MediatR;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<long>
    {
        public CreateProductRequest Request { get; set; }
    }  
}
