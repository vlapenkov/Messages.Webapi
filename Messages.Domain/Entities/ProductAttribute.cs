using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Messages.Domain.Models
{
    /// <summary>
    /// Атрибут
    /// </summary>
    public  class ProductAttribute :BaseEntity
    {
        //private ProductAttribute() {}

        public ProductAttribute(long id, string name)
        {             
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        [StringLength(512)]
        public string Name { get; private set; }
    }
}
