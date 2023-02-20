using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    public record RegisterProductsExchangeRequest
    {
        public int ExchangeType { get; set; }

        public long ProductsLoaded { get; set; }
    }
}
