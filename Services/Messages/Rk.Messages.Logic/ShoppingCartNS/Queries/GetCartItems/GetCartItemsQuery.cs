using MediatR;
using Rk.Messages.Logic.ShoppingCartNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ShoppingCartNS.Queries.GetCartItems
{
    public class GetCartItemsQuery : IRequest<IReadOnlyCollection<ShoppingCartItemDto>>
    {        
    }
}
