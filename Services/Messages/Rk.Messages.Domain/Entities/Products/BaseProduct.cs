using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Базовый класс для всех продуктов/услуг/технологий
    /// </summary>
    public abstract class BaseProduct : AuditableEntity
    {
        protected BaseProduct() { }
        public BaseProduct(long organizationId, long catalogSectionId,  string name, string description, IReadOnlyCollection<AttributeValue> attributeValues)
        {
            CatalogSectionId = catalogSectionId;          
            Name = name;
            Description = description;
            OrganizationId = organizationId;

            _attributeValues = attributeValues.ToList();
            
        }        

        /// <summary>Раздел каталога</summary>
        public long CatalogSectionId { get; protected set; }

        public long OrganizationId { get; protected set; }

        public Organization Organization{ get; }

        public virtual CatalogSection CatalogSection { get;  }

        /// <summary>Наименование продукции</summary>
        [StringLength(512)]
        public string Name { get; protected set; }

        /// <summary>Описание продукции</summary>
        [StringLength(1024)]
        public string Description { get; protected set; }
        

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
    }
}
