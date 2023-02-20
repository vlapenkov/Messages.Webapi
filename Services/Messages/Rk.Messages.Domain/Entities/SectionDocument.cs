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
    /// Документ секции (раздела)
    /// </summary>
    public class SectionDocument :BaseEntity
    {
        protected SectionDocument() { }
        public SectionDocument(Document document)
        {
            Document = document;
        }       

        public long CatalogSectionId { get; private set; }

        public virtual CatalogSection Section { get; } = null!;

        public long DocumentId { get; private set; }

        public virtual Document Document { get; } = null!;

    }
}
