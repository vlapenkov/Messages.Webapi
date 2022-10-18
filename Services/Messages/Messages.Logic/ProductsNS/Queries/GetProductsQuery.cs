using MediatR;
using Messages.Logic.CommonNS.Dto;
using Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Messages.Logic.ProductsNS.Queries
{
    public class GetProductsQuery : IRequest<PagedResponse<ProductShortDto>>
    {
        public FilterProductsRequest Request { get; set; }
    }
}
