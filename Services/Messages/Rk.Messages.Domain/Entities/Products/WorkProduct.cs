using System.Collections.Generic;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Работа – деятельность исполнителя, приводящая к созданию конечного продукта – товара
    /// </summary>
    public class WorkProduct : BaseProduct
    {
        protected WorkProduct() { }
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
        
        #region Private Members
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Используются ли иностранные компоненты
        /// </summary>
        /// <param name="value"></param>
        public void SetAreForeignComponentsUsed(bool value)
        {
            AreForeignComponentsUsed = value;
        }
        
        #endregion

        public override string GetProductionType()
        {
            return nameof(WorkProduct);
        }
    }
}
