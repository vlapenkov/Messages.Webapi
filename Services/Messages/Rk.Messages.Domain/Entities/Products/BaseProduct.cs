using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Базовый класс для всех продуктов/услуг/технологий
    /// </summary>
    public abstract class BaseProduct : BaseEntity
    {
        protected BaseProduct() { }
        public BaseProduct(long catalogSectionId,  string name, string description, IReadOnlyCollection<AttributeValue> attributeValues)
        {
            CatalogSectionId = catalogSectionId;          
            Name = name;
            Description = description;
            _attributeValues = attributeValues.ToList();
            
        }

        public BaseProduct(long catalogSectionId, string name, string description)
        {
            CatalogSectionId = catalogSectionId;
            Name = name;
            Description = description;
        }

        /// <summary>Раздел каталога</summary>
        public long CatalogSectionId { get; protected set; }
        public virtual CatalogSection CatalogSection { get;  }

        /// <summary>Наименование продукции</summary>
        [StringLength(512)]
        public string Name { get; protected set; }

        /// <summary>Описание продукции</summary>
        [StringLength(1024)]
        public string Description { get; protected set; }

        /// <summary>Значения атрибутов</summary>
        //public virtual IReadOnlyCollection<AttributeValue> AttributeValues { get; protected set; }

        private readonly List<AttributeValue> _attributeValues;
        public virtual IReadOnlyCollection<AttributeValue> AttributeValues => _attributeValues;
    }
}
