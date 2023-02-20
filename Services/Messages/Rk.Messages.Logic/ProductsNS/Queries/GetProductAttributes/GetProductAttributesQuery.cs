using MediatR;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using System.Collections.Generic;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductsQuery
{
    /// <summary>
    /// Получить список продукции
    /// </summary>
    public class GetProductAttributesQuery : IRequest<IReadOnlyCollection<AttributeDto>>
    {
       
    }
}
