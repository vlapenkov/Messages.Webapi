using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Domain.Models.Products
{
    /// <summary>
    /// Услуга
    /// </summary>
    public class ServiceProduct : BaseProduct
    {
        public ServiceProduct(int catalogSectionId, string name, string description, ICollection<AttributeValue> attributeValues) : base(catalogSectionId, name, description, attributeValues)
        {
        }
    }
}
