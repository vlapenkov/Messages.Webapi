
using Rk.Messages.Spa.Infrastructure.Dto.ProductsNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ShoppingCartNS
{
    public class ShoppingCartItemDto
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public Guid? DocumentId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Sum => Price * Quantity;

        public OrganizationShortDto Organization { get; set; }
    }
}
