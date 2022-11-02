using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    public class ProductShortDto : BaseDto
    {
        /// <summary>информация о продукте</summary>
        public string Description { get; set; }

        /// <summary>цена</summary>
        public decimal Price { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
