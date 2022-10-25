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
        
    }
}
