using Messages.Spa.Infrastructure.Dto.CommonNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Отбор по продуктам
    /// </summary>
    public record FilterProductsRequest : PagedRequest
    {
        /// <summary>по каталогу продукта</summary>
        public long? CatalogSectionId { get; set; }

        /// <summary>по наименованию</summary>
        public string? Name { get; set; }
        
    }
}
