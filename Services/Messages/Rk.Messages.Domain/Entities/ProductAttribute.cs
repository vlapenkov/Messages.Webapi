using System;
using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Атрибут
    /// </summary>
    public  class ProductAttribute :BaseEntity
    {    

        public ProductAttribute(long id, string name)
        {             
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        [StringLength(512)]
        public string Name { get; private set; }
    }
}
