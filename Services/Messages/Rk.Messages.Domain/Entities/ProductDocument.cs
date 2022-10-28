using Rk.Messages.Domain.Entities.Products;
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
        private ProductDocument() { }
        public ProductDocument(Document document)
        {
            Document = document;
        }

        public long BaseProductId { get; private set; }

        public virtual BaseProduct BaseProduct { get;  }

        public long DocumentId { get; private set; }

        public virtual Document Document { get; }

    }
}
