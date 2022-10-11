﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Messages.Domain.Models.Products
{
    /// <summary>
    /// Продукция
    /// </summary>
    public class Product : BaseProduct
    {
        public Product(int catalogSectionId, string name, string description, ICollection<AttributeValue> attributeValues) : base(catalogSectionId, name, description, attributeValues)
        {
        }

        [StringLength(256)]
        public string CodeTnVed { get; private set; }

       
       
    }
}
