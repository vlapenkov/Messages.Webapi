using Rk.Messages.Logic.CommonNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    public record ProductsExchangeDto :AuditableEntityDto
    {
        public string ProductExchangeText { get; set; }

        public long ProductsLoaded { get; set; }
    }
}
