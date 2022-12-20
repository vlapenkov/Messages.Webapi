using MediatR;
using Rk.Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductsExchanges
{
    public class GetProductsExchangesQuery :IRequest<IReadOnlyCollection<ProductsExchangeDto>>
    {

    }
}
