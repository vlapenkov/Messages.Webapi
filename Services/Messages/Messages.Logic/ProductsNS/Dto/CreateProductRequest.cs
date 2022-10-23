using Messages.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.ProductsNS.Dto
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

        /// <summary>Описание</summary>
        public string Description { get; set; }

        /// <summary>Значения атрибутов</summary>
        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        /// <summary>Код TnVed</summary>
        public string CodeTnVed { get; set; }

        /// <summary>Цена</summary>
        public decimal Price { get; set; }
    }
}
