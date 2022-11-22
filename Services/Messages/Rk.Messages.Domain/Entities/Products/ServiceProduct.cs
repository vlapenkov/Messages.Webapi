using System.Collections.Generic;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Услуга
    /// </summary>
    public class ServiceProduct : BaseProduct
    {
        private ServiceProduct() { }
        public ServiceProduct(
            long organizationId,
            long catalogSectionId, 
            string name, 
            string description, 
            IReadOnlyCollection<AttributeValue> attributeValues) : 
            base(organizationId,catalogSectionId, name, description, attributeValues)
        {
        }
    }
}
