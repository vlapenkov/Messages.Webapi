using MediatR;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductQuery
{
    /// <summary>
    /// Получить детальную информацию о продукции
    /// </summary>
    public class GetProductQuery : IRequest<ProductResponse>
    {
        /// <summary>Идентификатор продукции</summary>
        public long Id { get; set; }
    }
}
