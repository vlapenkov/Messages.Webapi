using System.Collections.Generic;

namespace Rk.Messages.Domain.Entities.Products
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
