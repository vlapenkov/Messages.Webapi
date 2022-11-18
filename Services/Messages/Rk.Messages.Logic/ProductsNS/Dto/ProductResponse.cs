using Rk.Messages.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public string FullName { get; set; }

        public long CatalogSectionId { get; set; }

        public string Description { get; set; }

        public string CodeTnVed { get; set; }

        public decimal Price { get; set; }        

        public string MeasuringUnit { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public ProductStatus Status { get; set; }

        public string StatusText { get; set; }


        /// <summary>значения атрибутов</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        /// <summary>документы</summary>
        public List<FileDataDto> Documents { get; set; } = new List<FileDataDto>();

       
    }
}
