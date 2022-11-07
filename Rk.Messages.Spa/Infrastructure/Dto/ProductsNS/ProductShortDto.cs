using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    public record ProductShortDto : AuditableEntityDto
    {
        /// <summary>идентификатор продукта</summary>
        public long Id { get; set; }

        /// <summary>информация о продукте</summary>
        public string Description { get; set; }

        /// <summary>цена</summary>
        public decimal Price { get; set; }

        /// <summary>Ссылка на документ</summary>
        public Guid? DocumentId { get; set; }

    }
}
