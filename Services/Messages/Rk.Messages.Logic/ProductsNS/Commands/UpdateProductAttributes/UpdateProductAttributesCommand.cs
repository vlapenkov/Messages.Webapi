using MediatR;
using Rk.Messages.Logic.ProductsNS.Dto;
using System.Collections.Generic;

namespace Rk.Messages.Logic.ProductsNS.Commands.UpdateProductAttributes
{
    /// <summary>
    /// Апдейт атрибутов
    /// </summary>
    public class UpdateProductAttributesCommand : IRequest
    {
        public long ProductId { get; set; }

        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }
    }
}
