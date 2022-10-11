using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Domain.Models.Products
{
    /// <summary>
    /// Технология
    /// </summary>
    public class Technology : BaseProduct
    {
        public Technology(int catalogSectionId, string name, string description, ICollection<AttributeValue> attributeValues) : base(catalogSectionId, name, description, attributeValues)
        {
        }
    }
}
