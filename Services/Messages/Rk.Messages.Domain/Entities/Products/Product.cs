using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Продукция
    /// </summary>
    public class Product : BaseProduct
    {
        public Product(long catalogSectionId, string name, string description,  decimal price) : base(catalogSectionId, name, description) 
        {
            Price = price;
        }
        public Product(long catalogSectionId, string name, string description, decimal price, IReadOnlyCollection<AttributeValue> attributeValues) : base(catalogSectionId, name, description, attributeValues)
        {
            Price = price;
        }

        [StringLength(256)]
        public string CodeTnVed { get; private set; }

        public decimal Price { get; private set; }

        /// <summary>
        /// Установить цену продукции
        /// </summary>
        /// <param name="price">новая цена</param>
        public void SetPrice(decimal price)
            { this.Price = price; }

    }
}
