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

        public long CatalogSectionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IReadOnlyCollection<AttributeValueDto> AttributeValues { get; set; }

        public string CodeTnVed { get; set; }

        public decimal Price { get; set; }
    }
}
