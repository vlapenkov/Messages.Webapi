using MediatR;
using Rk.Messages.Logic.ProductsNS.Dto;
using System.Collections.Generic;

namespace Rk.Messages.Logic.ProductsNS.Commands.UpdateProductAttributes
{
    /// <summary>
    /// Апдейт товара
    /// </summary>
    public class UpdateProductCommand : IRequest
    {
        public long ProductId { get; set; }

        public UpdateProductRequest Request { get; set; }
    }
}
