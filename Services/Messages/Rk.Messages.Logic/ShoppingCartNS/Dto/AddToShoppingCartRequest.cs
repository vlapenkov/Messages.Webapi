using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ShoppingCartNS.Dto
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
