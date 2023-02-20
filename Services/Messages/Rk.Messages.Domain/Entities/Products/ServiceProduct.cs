using System.Collections.Generic;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Услуга
    /// </summary>
    public class ServiceProduct : BaseProduct
    {
        protected ServiceProduct() { }
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
        
        #region Private Members
        
        #endregion
        
        #region Methods
        
        /// <summary>
        /// Используются ли иностранные компоненты
        /// </summary>
        /// <param name="value"></param>
        public void SetAreForeignComponentsUsed(bool value)
        {
            this.AreForeignComponentsUsed = value;
        }
        
        #endregion

        public override string GetProductionType()
        {
            return nameof(ServiceProduct);
        }
    }
}
