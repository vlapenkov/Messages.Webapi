using System.Collections.Generic;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Технология
    /// </summary>
    public class Technology : BaseProduct
    {
        private Technology() { }
        public Technology(
            long organizationId,
            long catalogSectionId, 
            string name, 
            string description, 
            IReadOnlyCollection<AttributeValue> attributeValues) : 
            base(organizationId, catalogSectionId, name, description, attributeValues)
        {
        }
    }
}
