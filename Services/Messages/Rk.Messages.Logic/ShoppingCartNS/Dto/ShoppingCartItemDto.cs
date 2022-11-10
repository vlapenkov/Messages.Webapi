using Rk.Messages.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
