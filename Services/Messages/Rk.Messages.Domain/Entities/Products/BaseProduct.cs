using Rk.Messages.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Базовый класс для всех товров/услуг/работ
    /// </summary>
    public abstract class BaseProduct : AuditableEntity
    {
        const short _maxDescriptionLength = 4096;
        protected BaseProduct() { }
        public BaseProduct(long organizationId, long catalogSectionId,  string name, string description, IReadOnlyCollection<AttributeValue> attributeValues)
        {
            CatalogSectionId = catalogSectionId;          
            Name = name;
            
            OrganizationId = organizationId;

            if (description != null)
            {
                int maxLength = description.Length > _maxDescriptionLength ? _maxDescriptionLength : description.Length;

                Description = description.Substring(0, maxLength);
            }
            _attributeValues = attributeValues.ToList();
            
        }        

        /// <summary>Раздел каталога</summary>
        public long CatalogSectionId { get; protected set; }

        public long OrganizationId { get; protected set; }

        public Organization Organization{ get; }

        public virtual CatalogSection CatalogSection { get;  }

        /// <summary>Наименование продукции</summary>
        [StringLength(512)]
        [Required]
        public string Name { get; protected set; }

        /// <summary>Описание продукции</summary>        
        [StringLength(4096)]
        public string Description { get; protected set; }

        /// <summary>Статус продукции (по умолчанию черновик)</summary>
        public ProductStatus Status { get; private set; } = ProductStatus.Draft;


        private readonly List<AttributeValue> _attributeValues = new List<AttributeValue>();
        public virtual IReadOnlyCollection<AttributeValue> AttributeValues => _attributeValues;

        private readonly List<ProductDocument> _productDocuments = new List<ProductDocument>();
        public virtual IReadOnlyList<ProductDocument> ProductDocuments => _productDocuments;

        /// <summary>
        /// Добавить информацию о файлах
        /// </summary>
        /// <param name="productFiles"></param>
        public void AddProductDocuments(IReadOnlyCollection<ProductDocument> productFiles) {

            _productDocuments.AddRange(productFiles);
        }

        /// <summary>
        /// Апдейт значений атрибутов
        /// </summary>        
        public void UpdateAttributeValues(IReadOnlyCollection<AttributeValue> attributeValues) {

            _attributeValues.Clear();

            _attributeValues.AddRange(attributeValues);
        }

        public ProductDocument GetProductDocument()=> _productDocuments.FirstOrDefault();

        public void SetStatus(ProductStatus newStatus) { 
        
            Status = newStatus;
        }

        public abstract string GetProductionType();
    }
}
