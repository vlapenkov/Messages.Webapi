using Rk.Messages.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Строка - информация о товаре в корзине
    /// </summary>
    public class ShoppingCartItem : AuditableEntity
    {
        public ShoppingCartItem(string userName, long productId,  decimal price, int quantity)
        {
            UserName = userName;
            ProductId = productId;            
            Price = price;
            Quantity = quantity;
        }

        [StringLength(255)]
        public string UserName { get; private set; }

        public long ProductId { get; private set; }

        public virtual BaseProduct Product { get; }

        public decimal Price { get; private set; }

        public int Quantity { get; private set; }

        public decimal Sum => Price * Quantity;

        public void Increment(int quantity) {
            Quantity += quantity;
        }

    }
}
