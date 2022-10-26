using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rk.Messages.Domain.Entities.Products;

namespace Rk.Messages.Domain.Entities
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

        /// <summary>Родительский id раздела</summary>
        public long? ParentCatalogSectionId { get; private set; }

        public virtual CatalogSection Parent { get; }


        /// <summary>Наименование раздела</summary>
        [StringLength(1024)]
        public string Name { get; private set; }

        /// <summary>Разделы внутри текущего</summary>
        private readonly List<CatalogSection> _children;
        public virtual IReadOnlyCollection<CatalogSection> Children => _children;

        //public virtual List<CatalogSection> Children { get; }

        private readonly List<BaseProduct> _products;
        public virtual IReadOnlyCollection<BaseProduct> Products => _products;
    }
}
