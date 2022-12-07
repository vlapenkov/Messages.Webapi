using Rk.Messages.Domain.Enums;
using Rk.Messages.Logic.CommonNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    /// <summary>
    /// Отбор по продуктам
    /// </summary>
    public record FilterProductsRequest : PagedRequest
    {
        /// <summary>по каталогу продукта</summary>
        public long? CatalogSectionId { get; set; }

        /// <summary>по наименованию</summary>
        public string Name { get; set; }

        /// <summary>по региону</summary>
        public string Region { get; set; }

        /// <summary>по производителю</summary>
        public string ProducerName { get; set; }

        /// <summary>по статусу</summary>
        public ProductStatus? Status { get; set; }

        /// <summary>по доступности</summary>
        public AvailableStatus? AvailableStatus { get; set; }

    }
}
