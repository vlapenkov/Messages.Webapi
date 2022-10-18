using System.Collections.Generic;

namespace Messages.Spa.Infrastructure.Dto.ProductsNS
{
    public record CreateProductRequest
    {

        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        public string CodeTnVed { get; set; }
    }
}
