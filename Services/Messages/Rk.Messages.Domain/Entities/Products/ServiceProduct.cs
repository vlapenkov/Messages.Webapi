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
            string fullname,
            string description, 
            decimal? price,
            IReadOnlyCollection<AttributeValue> attributeValues) : 
            base(organizationId,catalogSectionId, name, fullname, description, price, attributeValues)
        {
        }

        public override string GetProductionType()
        {
            return nameof(ServiceProduct);
        }
    }
}
