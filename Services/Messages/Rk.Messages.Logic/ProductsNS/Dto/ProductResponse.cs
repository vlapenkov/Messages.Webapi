using Rk.Messages.Domain.Enums;
using Rk.Messages.Logic.CommonNS.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    /// <summary>
    /// Информация о товаре
    /// </summary>
    public record ProductResponse: AuditableEntityDto
    {        

        /// <summary>наименование</summary>
        public string Name { get; set; }

        public string FullName { get; set; }

        public long CatalogSectionId { get; set; }

        public string Description { get; set; }

        public string CodeTnVed { get; set; }

        public string CodeOkpd2 { get; set; }

        public string Address { get; set; }

        public decimal? Price { get; set; }        

        public string MeasuringUnit { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public float? Rating { get; set; }

        //public ProductStatus Status { get; set; }

        public string StatusText { get; set; }

        public string AvailableStatusText { get; set; }

        public OrganizationShortDto Organization { get; set; }


        /// <summary>значения атрибутов</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        /// <summary>документы</summary>
        public List<FileDataDto> Documents { get; set; } = new List<FileDataDto>();

       
    }
}
