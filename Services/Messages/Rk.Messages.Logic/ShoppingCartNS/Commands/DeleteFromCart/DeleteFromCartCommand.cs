using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ShoppingCartNS.Commands.DeleteFromCart
{
    public class DeleteFromCartCommand : IRequest
    {
        public long ProductId { get; set; }
    }
}
