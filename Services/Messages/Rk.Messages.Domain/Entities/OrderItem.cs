using Rk.Messages.Domain.Entities.Products;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Единица
    /// </summary>
    public class OrderItem : BaseEntity
    {
        public OrderItem(long productId,  decimal price, int quantity)
        {
            ProductId = productId;            
            Price = price;
            Quantity = quantity;
        }

        public long OrderId { get; private set; }

        public virtual Order Order { get; }

        public long ProductId { get; private set; }

        public virtual BaseProduct Product { get; }

        public decimal Price { get; private set; }

        public int Quantity { get; private set; }

        public decimal Sum => Price * Quantity;
    }
}
