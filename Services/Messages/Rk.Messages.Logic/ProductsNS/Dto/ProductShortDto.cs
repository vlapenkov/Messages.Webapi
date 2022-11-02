using Rk.Messages.Logic.CommonNS.Dto;
using System;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    public record ProductShortDto :BaseDto
    {
        /// <summary>информация о продукте</summary>
        public string Description { get; set; }

        /// <summary>цена</summary>
        public decimal Price { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
