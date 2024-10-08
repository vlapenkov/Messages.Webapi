﻿using Rk.Messages.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    public record RegisterProductsExchangeRequest
    {
        public ProductExchangeType ExchangeType { get; set; }

        public long ProductsLoaded { get;  set; }
    }
}
