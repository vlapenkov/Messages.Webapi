using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rk.Messages.Domain.Entities.Products;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Раздел каталога
    /// </summary>
    public class CatalogSection :AuditableEntity
    {
        public CatalogSection(long? parentCatalogSectionId,  string name)
        {
            ParentCatalogSectionId = parentCatalogSectionId;           
            Name = name;            
        }

        /// <summary>Родительский id раздела</summary>
        public long? ParentCatalogSectionId { get; private set; }

        public virtual CatalogSection Parent { get; } = null!;


        /// <summary>Наименование раздела</summary>
        [StringLength(1024)]
        public string Name { get; private set; }

        /// <summary>Разделы внутри текущего</summary>
        private readonly List<CatalogSection> _children = new();
        public virtual IReadOnlyCollection<CatalogSection> Children => _children;

        /// <summary>Продукция внутри текущего раздела</summary>
        private readonly List<BaseProduct> _products = new();
        public virtual IReadOnlyCollection<BaseProduct> Products => _products;

        /// <summary>Документы текущего раздела</summary>
        private readonly List<SectionDocument> _sectionDocuments = new List<SectionDocument>();
        public virtual IReadOnlyCollection<SectionDocument> SectionDocuments => _sectionDocuments;

        public void UpsertDocument(SectionDocument document) { 
            _sectionDocuments.Add(document);
        }
    }
}
