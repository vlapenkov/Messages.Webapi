using System.Collections.Generic;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.ServiceProductsNS.Dto
{
    /// <summary>
    /// Создание запроса услуги
    /// </summary>
    public record CreateServiceProductRequest
    {
        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        /// <summary>Информация о файлах для услуги</summary>
        public List<FileDataDto> Documents { get; set; } 
    }
}
