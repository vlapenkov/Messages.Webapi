using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    /// <summary>
    /// Создание продукта
    /// </summary>
    public record CreateProductRequest
    {
        /// <summary>Id раздела</summary>
        public long CatalogSectionId { get; set; }

        /// <summary>Наименование</summary>
        public string Name { get; set; }

        /// <summary>Полное Наименование</summary>
        public string FullName { get; set; }

        /// <summary>Описание</summary>
        public string Description { get; set; }             

        /// <summary>Код TnVed</summary>
        public string CodeTnVed { get; set; }
                
        public string CodeOkpd2 { get; set; }
        
        public string Address { get; set; }

        public string Article { get; set; }

        /// <summary>Цена</summary>
        public decimal? Price { get; set; }
        
        public float? ShareOfForeignComponents { get; set; }

        /// <summary>Значения атрибутов</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        public List<FileDataDto> Documents { get; set; }
    }
}
