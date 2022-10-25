using Rk.Messages.Logic.CommonNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Dto
{
    /// <summary>
    /// Информация о товаре для списка
    /// </summary>
    public class ProductShortDto :BaseDto
    {
        /// <summary>информация о продукте</summary>
        public string Description { get; set; }

        /// <summary>цена</summary>
        public decimal Price { get; set; }
    }
}
