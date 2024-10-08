﻿using Rk.Messages.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Файл продукта
    /// </summary>
    public class ProductDocument :BaseEntity
    {
       
        public ProductDocument(Document document)
        {
            Document = document;
        }

        public ProductDocument(long baseProductId, long documentId)
        {
            BaseProductId = baseProductId;
          
            DocumentId = documentId;
        }

        public long BaseProductId { get; private set; }

        public virtual BaseProduct BaseProduct { get; } = null!;

        public long DocumentId { get; private set; }

        public virtual Document Document { get; } = null!;

    }
}
