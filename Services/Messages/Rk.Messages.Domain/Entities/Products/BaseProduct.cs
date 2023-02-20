using Rk.Messages.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace Rk.Messages.Domain.Entities.Products
{
    /// <summary>
    /// Базовый класс для всех товров/услуг/работ
    /// </summary>
    public abstract class BaseProduct : AuditableEntity
    {        
        protected BaseProduct() { }
        public BaseProduct(long organizationId, long catalogSectionId,  string name, string fullName, string description, decimal? price, IReadOnlyCollection<AttributeValue> attributeValues)
        {
            CatalogSectionId = catalogSectionId;   
            
            Name = name;

            FullName = fullName;

            Description = description;

            OrganizationId = organizationId;
           
            Price = price;

            _attributeValues =  attributeValues?.ToList() ?? new List<AttributeValue>();

        }        

        /// <summary>Раздел каталога</summary>
        public long CatalogSectionId { get; protected set; }

        /// <summary>Организация</summary>
        public long OrganizationId { get; protected set; }

        public virtual Organization Organization { get; } = null!;

        public virtual CatalogSection CatalogSection { get; } = null!;

        /// <summary>Наименование продукции</summary>
        [StringLength(512)]

        public string Name { get; protected set; } = null!;


        [StringLength(1024)]
        public string? FullName { get; protected set; }

        /// <summary>Описание продукции</summary>        
        [StringLength(4096)]
        public string? Description { get; protected set; }

        [StringLength(1024)]
        public string? Article { get; protected set; }

        /// <summary>Цена, если null, то договорная</summary>        
        public decimal? Price { get; protected set; }

        /// <summary>Статус продукции (по умолчанию черновик)</summary>
        public ProductStatus Status { get; protected set; } = ProductStatus.Draft;

        /// <summary>Рейтинг</summary>
        public float? Rating { get; protected set; } = 0f;
        
        /// <summary>
        /// Используются ли иностранные компоненты
        /// </summary>
        public bool? AreForeignComponentsUsed { get; protected set; } = false;

       
        /// <summary>
        /// Документы продукции
        /// </summary>
        private readonly List<ProductDocument> _productDocuments = new List<ProductDocument>();
        public virtual IReadOnlyList<ProductDocument> ProductDocuments => _productDocuments;


        /// <summary>
        /// Значения атрибутов
        /// </summary>
        private readonly List<AttributeValue> _attributeValues = new List<AttributeValue>();
        public virtual IReadOnlyCollection<AttributeValue> AttributeValues => _attributeValues;


        /// <summary>
        /// Отзывы
        /// </summary>
        private readonly List<Review> _reviews = new List<Review>();
        public virtual IReadOnlyCollection<Review> Reviews => _reviews;


        /// <summary>
        /// Апдейт значений атрибутов
        /// </summary>        
        public void UpdateAttributeValues(IReadOnlyCollection<AttributeValue> attributeValues)
        {

            _attributeValues.Clear();

            _attributeValues.AddRange(attributeValues);
        }

        /// <summary>
        /// Добавить информацию о файлах
        /// </summary>        
        public void AddProductDocuments(IReadOnlyCollection<ProductDocument> productFiles) {

            _productDocuments.AddRange(productFiles);
        }

        /// <summary>
        /// Добавить отзыв
        /// </summary>        
        public void AddReview(Review review)
        {
            _reviews.Add(review);
            
            Rating =  _reviews.Sum(x => x.Rating) / _reviews.Count;
                        
        }
        
        public ProductDocument GetProductDocument()=> _productDocuments.First();

        public void SetStatus(ProductStatus newStatus) { 
        
            Status = newStatus;
        }

        /// <summary>
        /// Обновить поля
        /// </summary>
        public virtual void Update(long catalogSectionId, string name, string fullName, string description, decimal? price) {
            
            CatalogSectionId = catalogSectionId;
            Name = name;
            FullName = fullName;
            Description = description;
            Price = price;
        }

        public void SetArticle(string article)
        {
            Article = article;
           
        }

        public abstract string GetProductionType();
    }
}
