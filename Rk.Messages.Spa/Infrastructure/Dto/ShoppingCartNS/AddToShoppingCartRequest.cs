namespace Rk.Messages.Spa.Infrastructure.Dto.ShoppingCartNS
{
    /// <summary>
    /// Запрос для добавления в корзину
    /// </summary>
    public record AddToShoppingCartRequest
    {
        /// <summary>Id продукции</summary>
        public long ProductId { get; set; }

        /// <summary>Количество</summary>
        public int Quantity { get; set; }
    }
}
