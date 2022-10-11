using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Messages.Domain.Models.Products
{
    /// <summary>
    /// Базовый класс для всех продуктов/услуг/технологий
    /// </summary>
    public class BaseProduct : BaseEntity
    {
        public BaseProduct(int catalogSectionId,  string name, string description, ICollection<AttributeValue> attributeValues)
        {
            CatalogSectionId = catalogSectionId;          
            Name = name;
            Description = description;
            AttributeValues = attributeValues;
        }

        public int CatalogSectionId { get; protected set; }
        public virtual CatalogSection CatalogSection { get;  }

        [StringLength(512)]
        public string Name { get; protected set; }

        [StringLength(1024)]
        public string Description { get; protected set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; protected set; }
    }
}
