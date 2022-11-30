using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Информация о товаре
    /// </summary>
    public record ProductResponse :AuditableEntityDto
    {
        public long Id { get; set; }

        /// <summary>наименование</summary>
        public string Name { get; set; }

        public string FullName { get; set; }

        public long CatalogSectionId { get; set; }

        public string Description { get; set; }

        public string CodeTnVed { get; set; }

        public decimal Price { get; set; }     

        public string MeasuringUnit { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public string StatusText { get; set; }

        public OrganizationShortDto Organization { get; set; }

        /// <summary>атрибуты</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        /// <summary>документы</summary>
        public FileDataDto[] Documents { get; set; }

       
    }
}
