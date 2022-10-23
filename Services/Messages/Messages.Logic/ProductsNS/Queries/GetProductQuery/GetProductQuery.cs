using MediatR;
using Messages.Logic.CommonNS.Dto;
using Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Messages.Logic.ProductsNS.Queries.GetProductQuery
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
