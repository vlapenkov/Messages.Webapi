using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Строка - информация о товаре в заказе
    /// </summary>
    public class Order :AuditableEntity
    {
        public Order(long organizationId,  string userName)
        {
         
            OrganizationId = organizationId;            
            UserName = userName;
        }

        /// <summary>Id организация покупателя</summary>
        public long OrganizationId { get; private set; }

        /// <summary>Организация покупателя</summary>
        public virtual Organization Organization { get;}

        [StringLength(255)]
        public string UserName { get; private set; }

        [StringLength(1024)]
        public string Comments { get; private set; }

        /// <summary>Состав заказа</summary>
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public virtual IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AddOrderItems(IReadOnlyCollection<OrderItem> orderItems)
        {

            _orderItems.AddRange(orderItems);
        }

    }
}
