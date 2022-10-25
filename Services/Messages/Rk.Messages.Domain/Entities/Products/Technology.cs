using System.Collections.Generic;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Технология
    /// </summary>
    public class Technology : BaseProduct
    {
        private Technology() { }
        public Technology(int catalogSectionId, string name, string description, IReadOnlyCollection<AttributeValue> attributeValues) : base(catalogSectionId, name, description, attributeValues)
        {
        }
    }
}
