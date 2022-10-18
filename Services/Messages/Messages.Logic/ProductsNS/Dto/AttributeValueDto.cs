﻿using Messages.Domain.Models.Products;
using Messages.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Messages.Logic.ProductsNS.Dto
{
    public record AttributeValueDto
    {
        /// <summary>Id товара</summary>
        public long BaseProductId { get; set; }

        /// <summary>Id атрибута</summary>
        public long AttributeId { get; set; }

        /// <summary>Значение атрибута</summary>
        public string Value { get;  set; }

    }
}