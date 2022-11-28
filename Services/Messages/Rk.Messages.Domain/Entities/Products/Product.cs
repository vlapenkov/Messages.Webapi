using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rk.Messages.Domain.Enums;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Продукция
    /// </summary>
    public class Product : BaseProduct
    {
        protected Product() { }
       
        public Product(long organizationId,  
            long catalogSectionId, 
            string name,
            string fullname,
            string description, 
            decimal price, 
            IReadOnlyCollection<AttributeValue> attributeValues
       
            ) 
            : base(organizationId, catalogSectionId, name, description, attributeValues)
        {
            FullName = fullname;
            Price = price;
            
        }

        #region Private Members

        [StringLength(256)]
        public string CodeTnVed { get; private set; }

        public decimal Price { get; private set; }

        [StringLength(1024)]
        public string FullName { get; private set; }


        [StringLength(128)]
        public string MeasuringUnit { get; private set; } = "шт.";

        [StringLength(128)]
        public string Country { get; private set; } = "Россия";

        [StringLength(3)]
        public string Currency { get; private set; } = "RUB";

        public ProductStatus Status { get; private set; } = ProductStatus.OnStock;



        private readonly List<string> _applicationAreas = new List<string>();
        public virtual IReadOnlyCollection<string> ApplicationAreas => _applicationAreas;


        #endregion

        /// <summary>
        /// Установить цену продукции
        /// </summary>
        /// <param name="price">новая цена</param>
        public void SetPrice(decimal price)
        { 
            this.Price = price; 
        }

        /// <summary>
        /// Добавить области применения
        /// </summary>        
        public void SetApplicationAreas(IReadOnlyCollection<string> applicationAreas)
        {
            this._applicationAreas.AddRange( applicationAreas);
        }



    }
}
