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
        [StringLength(512)]
        public string Name { get; private set; }
    }
}
