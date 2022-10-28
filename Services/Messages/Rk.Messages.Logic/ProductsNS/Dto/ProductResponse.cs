using System.Collections.Generic;

namespace Rk.Messages.Logic.ProductsNS.Dto
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

        /// <summary>значения атрибутов</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        /// <summary>документы</summary>
        public List<FileDataDto> Documents { get; set; } = new List<FileDataDto>();

        public string CodeTnVed { get; set; }

        public decimal Price { get; set; }
    }
}
