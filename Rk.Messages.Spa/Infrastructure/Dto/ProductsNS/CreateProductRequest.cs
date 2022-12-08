using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Создание запроса продукта
    /// </summary>
    public record CreateProductRequest
    {

        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        /// <summary>Значения атрибутов</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; } = new List<AttributeValueDto>();

        public string CodeTnVed { get; set; }

        public decimal? Price { get; set; }

        /// <summary>Информация о файлах для продукции</summary>
        public List<FileDataDto> Documents { get; set; } = new List<FileDataDto>();

    }
}
