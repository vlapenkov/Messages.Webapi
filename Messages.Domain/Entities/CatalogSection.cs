using Messages.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Messages.Domain.Models
{
    /// <summary>
    /// Раздел каталога
    /// </summary>
    public class CatalogSection :BaseEntity
    {
        public CatalogSection(long? parentCatalogSectionId,  string name)
        {
            ParentCatalogSectionId = parentCatalogSectionId;           
            Name = name;
        }

        public long? ParentCatalogSectionId { get; private set; }

        public virtual CatalogSection Parent { get; }

        [StringLength(1024)]
        public string Name { get; private set; }

        public virtual List<CatalogSection> Children { get; }

        public virtual List<BaseProduct> Products { get; }
    }
}
