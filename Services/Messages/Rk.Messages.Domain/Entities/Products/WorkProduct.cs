using System.Collections.Generic;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Работа – деятельность исполнителя, приводящая к созданию конечного продукта – товара
    /// </summary>
    public class WorkProduct : BaseProduct
    {
        private WorkProduct() { }
        public WorkProduct(
            long organizationId,
            long catalogSectionId,
            string name,
            string fullname,
            string description,
            decimal? price,
            IReadOnlyCollection<AttributeValue> attributeValues
            ) : 
            base(organizationId, catalogSectionId, name, fullname, description, price, attributeValues)
        {
        }

        public override string GetProductionType()
        {
            return nameof(WorkProduct);
        }
    }
}
