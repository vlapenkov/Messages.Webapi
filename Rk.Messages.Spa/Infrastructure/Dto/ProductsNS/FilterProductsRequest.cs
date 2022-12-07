using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Отбор по продуктам
    /// </summary>
    public record FilterProductsRequest : PagedRequest
    {
        /// <summary>по каталогу продукта</summary>
        public long? CatalogSectionId { get; set; }

        /// <summary>по наименованию</summary>
        public string? Name { get; set; }

        /// <summary>по региону</summary>
        public string? Region { get; set; }

        /// <summary>по производителю</summary>
        public string? ProducerName { get; set; }

        /// <summary>по статусу</summary>
        public int? Status { get; set; }

        /// <summary>по доступности</summary>
        public int? AvailableStatus { get; set; }

    }
}
