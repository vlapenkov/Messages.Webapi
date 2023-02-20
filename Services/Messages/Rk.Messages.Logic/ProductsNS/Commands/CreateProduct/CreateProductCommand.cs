using MediatR;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Commands.CreateProduct
{
    /// <summary>
    /// Команда создания товара
    /// </summary>
    public class CreateProductCommand : IRequest<long>
    {
        public CreateProductRequest Request { get; set; }
    }  
}
