using System.Collections.Generic;

namespace Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Создание запроса продукта
    /// </summary>
    public record CreateProductRequest
    {

        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; } = new List<AttributeValueDto>();

        public string CodeTnVed { get; set; }

        public decimal Price { get; set; }
    }
}
