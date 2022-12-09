

using Rk.Messages.Logic.ProductsNS.Dto;
using System.Collections.Generic;

namespace Rk.Messages.Logic.WorkProductsNS.Dto
{
    /// <summary>
    /// Создание запроса работы
    /// </summary>
    public record UpdateWorkProductRequest
    {
        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }
       
    }
}
