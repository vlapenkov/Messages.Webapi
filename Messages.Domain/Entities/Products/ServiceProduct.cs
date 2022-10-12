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
        private ServiceProduct() { }
        public ServiceProduct(int catalogSectionId, string name, string description, IReadOnlyCollection<AttributeValue> attributeValues) : base(catalogSectionId, name, description, attributeValues)
        {
        }
    }
}
