using MediatR;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductsQuery
{
    /// <summary>
    /// Получить список продукции
    /// </summary>
    public class GetProductsQuery : IRequest<PagedResponse<ProductShortDto>>
    {
        /// <summary>Отборы к запросу</summary>
        public FilterProductsRequest Request { get; set; }
    }
}
