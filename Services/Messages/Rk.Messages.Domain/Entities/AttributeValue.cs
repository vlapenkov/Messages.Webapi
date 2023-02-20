using System.ComponentModel.DataAnnotations;
using Rk.Messages.Domain.Entities.Products;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Значение атрибута товара
    /// </summary>
    public class AttributeValue: BaseEntity
    {       

        public AttributeValue( long attributeId, string value)
        {            
            AttributeId = attributeId;
            Value = value;
        }
               

        public long  BaseProductId { get; private set; }
        public virtual BaseProduct BaseProduct { get; set; } = null!;


        public long AttributeId { get; private set; }
        public virtual ProductAttribute Attribute { get; set; } = null!;

        [StringLength(1024)]
        public string Value { get; private set; }

    }
}
