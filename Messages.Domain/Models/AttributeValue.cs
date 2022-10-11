using Messages.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Messages.Domain.Models
{
    /// <summary>
    /// Значение атрибута товара
    /// </summary>
    public class AttributeValue: BaseEntity
    {
        public AttributeValue(long baseProductId, long attributeId,  string value)
        {
            BaseProductId = baseProductId;           
            AttributeId = attributeId;           
            Value = value;
        }

        private  AttributeValue()
        {}

        public long  BaseProductId { get; private set; }
        public virtual BaseProduct BaseProduct { get; set; }


        public long AttributeId { get; private set; }
        public virtual ProductAttribute Attribute { get; set; }

        [StringLength(1024)]
        public string Value { get; private set; }

    }
}
