
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.WorkProductsNS.Dto
{
    /// <summary>
    /// Информация о товаре
    /// </summary>
    public record WorkProductResponse : AuditableEntityDto
    {
        /// <summary>наименование</summary>
        public string Name { get; set; }

        public string FullName { get; set; }

        public long CatalogSectionId { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string StatusText { get; set; }

        public OrganizationShortDto Organization { get; set; }

        /// <summary>документы</summary>
        public FileDataDto[] Documents { get; set; }


    }
}
