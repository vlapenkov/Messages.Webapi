using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Информация о товаре
    /// </summary>
    public record ProductResponse
    {
        public long Id { get; set; }

        /// <summary>наименование</summary>
        public string Name { get; set; }

        public long CatalogSectionId { get; set; }

        public string Description { get; set; }

        /// <summary>атрибуты</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        /// <summary>документы</summary>
        public IReadOnlyCollection<FileDataDto> Documents { get; set; }

        public string CodeTnVed { get; set; }

        public decimal Price { get; set; }
    }
}
