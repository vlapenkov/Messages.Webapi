

using Rk.FileStore.Interfaces.Dto;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Создание запроса продукта
    /// </summary>
    public record ProductDto    {

        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }     

        public decimal Price { get; set; }

        /// <summary>Информация о файлах для продукции</summary>
        public List<FileDataDto> Documents { get; set; } = new List<FileDataDto>();

    }
}
