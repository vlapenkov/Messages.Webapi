using Rk.Messages.Logic.ProductsNS.Dto;
using System;

namespace Rk.Messages.Logic.ShoppingCartNS.Dto
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
