using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Messages.Domain.Models.Products
{
    /// <summary>
    /// Базовый класс для всех продуктов/услуг/технологий
    /// </summary>
    public abstract class BaseProduct : BaseEntity
    {
        protected BaseProduct() { }
        public BaseProduct(long catalogSectionId,  string name, string description, ICollection<AttributeValue> attributeValues)
        {
            CatalogSectionId = catalogSectionId;          
            Name = name;
            Description = description;
            AttributeValues = attributeValues;
        }

        public long CatalogSectionId { get; protected set; }
        public virtual CatalogSection CatalogSection { get;  }

        [StringLength(512)]
        public string Name { get; protected set; }

        [StringLength(1024)]
        public string Description { get; protected set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; protected set; }
    }
}
