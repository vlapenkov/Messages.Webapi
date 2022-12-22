using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ServiceProductNS
{
    /// <summary>
    /// Информация о услуге
    /// </summary>
    public record ServiceProductResponse : AuditableEntityDto
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
        
        public float? ShareOfForeignComponents { get; set; }

       
    }
}
