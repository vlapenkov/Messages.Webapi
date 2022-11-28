using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Commands.DeleteProduct
{
    public class DeleteProductCommand :IRequest
    {
        public long ProductId { get; set; }
    }
}
