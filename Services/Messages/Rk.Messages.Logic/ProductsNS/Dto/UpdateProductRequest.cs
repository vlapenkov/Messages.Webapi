using System.Collections.Generic;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    /// <summary>
    /// Апдейт товара
    /// </summary>
    public record UpdateProductRequest
    {
        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }       

        public string CodeTnVed { get; set; }

        public string CodeOkpd2 { get; set; }

        public string Address { get; set; }

        public string Article { get; set; }

        /// <summary>Значения атрибутов</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

    }
}
