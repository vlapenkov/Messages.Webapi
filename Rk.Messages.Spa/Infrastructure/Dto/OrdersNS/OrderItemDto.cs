using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Spa.Infrastructure.Dto.OrdersNS
{
    public record OrderItemDto
    {
        /// <summary>Id товара</summary>
        public long ProductId { get; set; }

        /// <summary>наименование товара</summary>
        public string ProductName { get; set; }

        /// <summary>цена</summary>
        public decimal Price { get; set; }

        /// <summary>количество</summary>
        public int Quantity { get; set; }

        public decimal Sum => Price * Quantity;
    }
}
