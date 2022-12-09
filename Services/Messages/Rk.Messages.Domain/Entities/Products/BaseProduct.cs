using Rk.Messages.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Базовый класс для всех товров/услуг/работ
    /// </summary>
    public abstract class BaseProduct : AuditableEntity
    {        
        protected BaseProduct() { }
        public BaseProduct(long organizationId, long catalogSectionId,  string name, string fullName, string description, decimal? price, IReadOnlyCollection<AttributeValue> attributeValues)
        {
            CatalogSectionId = catalogSectionId;   
            
            Name = name;

            FullName = fullName;

            Description = description;

            OrganizationId = organizationId;
           
            Price = price;

            _attributeValues =  attributeValues?.ToList() ?? new List<AttributeValue>();

        }        

        /// <summary>Раздел каталога</summary>
        public long CatalogSectionId { get; protected set; }

        /// <summary>Организация</summary>
        public long OrganizationId { get; protected set; }

        public Organization Organization{ get; }

        public virtual CatalogSection CatalogSection { get;  }

        /// <summary>Наименование продукции</summary>
        [StringLength(512)]
        [Required]
        public string Name { get; protected set; }


        [StringLength(1024)]
        public string FullName { get; protected set; }

        /// <summary>Описание продукции</summary>        
        [StringLength(4096)]
        public string Description { get; protected set; }

        /// <summary>Цена, если null, то договорная</summary>        
        public decimal? Price { get; protected set; }

        /// <summary>Статус продукции (по умолчанию черновик)</summary>
        public ProductStatus Status { get; protected set; } = ProductStatus.Draft;


       

        private readonly List<ProductDocument> _productDocuments = new List<ProductDocument>();
        public virtual IReadOnlyList<ProductDocument> ProductDocuments => _productDocuments;


        private readonly List<AttributeValue> _attributeValues = new List<AttributeValue>();
        public virtual IReadOnlyCollection<AttributeValue> AttributeValues => _attributeValues;


        /// <summary>
        /// Апдейт значений атрибутов
        /// </summary>        
        public void UpdateAttributeValues(IReadOnlyCollection<AttributeValue> attributeValues)
        {

            _attributeValues.Clear();

            _attributeValues.AddRange(attributeValues);
        }

        /// <summary>
        /// Добавить информацию о файлах
        /// </summary>
        /// <param name="productFiles"></param>
        public void AddProductDocuments(IReadOnlyCollection<ProductDocument> productFiles) {

            _productDocuments.AddRange(productFiles);
        }

       

        public ProductDocument GetProductDocument()=> _productDocuments.FirstOrDefault();

        public void SetStatus(ProductStatus newStatus) { 
        
            Status = newStatus;
        }

        public  virtual void Update(long catalogSectionId, string name, string fullName, string description, decimal? price) {
            
            CatalogSectionId = catalogSectionId;
            Name = name;
            FullName = fullName;
            Description = description;
            Price = price;
        }

        public abstract string GetProductionType();
    }
}
