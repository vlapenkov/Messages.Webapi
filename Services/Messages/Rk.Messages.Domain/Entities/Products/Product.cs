using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Rk.Messages.Domain.Enums;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product : BaseProduct
    {
        protected Product() { }
       
        public Product(long organizationId,  
            long catalogSectionId, 
            string name,
            string fullname,
            string description, 
            decimal? price, 
            IReadOnlyCollection<AttributeValue> attributeValues
       
            ) 
            : base(organizationId, catalogSectionId, name, fullname, description, price, attributeValues)
        { 
           
        }

        #region Private Members

        [StringLength(256)]
        public string CodeTnVed { get; private set; }

        [StringLength(256)]
        public string CodeOkpd2 { get; private set; }

        [StringLength(1024)]
        public string Article { get; private set; }

        /// <summary>Адрес производства</summary>
        [StringLength(4096)]
        public string Address { get; private set; }

        [StringLength(128)]
        public string MeasuringUnit { get; private set; } = "шт.";

        [StringLength(128)]
        public string Country { get; private set; } = "Россия";

        [StringLength(3)]
        public string Currency { get; private set; } = "RUB";

        [Required]
        public AvailableStatus AvailableStatus { get; private set; } = AvailableStatus.OnStock;


        private readonly List<string> _applicationAreas = new List<string>();
        public virtual IReadOnlyCollection<string> ApplicationAreas => _applicationAreas;



        #endregion

        #region Methods
       

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

        /// <summary>
        /// Установить статус достпности
        /// </summary>        
        public void SetAvailableStatus(AvailableStatus availableStatus)
        { 
            AvailableStatus = availableStatus;
        }

        public override string GetProductionType()
        {
            return nameof(Product);
        }

        public Product SetCodeTnVed(string codeTnVed)
        {
           CodeTnVed = codeTnVed;
            return this;
        }

        public Product SetCodeOkpd2(string codeOkpd2)
        {
            CodeOkpd2 = codeOkpd2;
            return this;
        }

        public Product SetAddress(string address)
        {
            Address = address;
            return this;
        }

        public Product SetArticle(string article)
        {
            Article = article;
            return this;
        }



        #endregion
    }
}
