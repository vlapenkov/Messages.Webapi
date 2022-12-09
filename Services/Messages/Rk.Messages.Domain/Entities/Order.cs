using Rk.Messages.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Строка - информация о товаре в заказе
    /// </summary>
    public class Order : AuditableEntity
    {
        public Order(long organizationId, long producerId, string userName)
        {

            OrganizationId = organizationId;

            ProducerId = producerId;

            UserName = userName;
        }

        /// <summary>Id организация покупателя</summary>
        public long OrganizationId { get; private set; }

        /// <summary>Покупатель</summary>
        public virtual Organization Organization { get; }


        /// <summary>Id организация производителя</summary>
        public long ProducerId { get; private set; }

        /// <summary>Покупатель</summary>
        public virtual Organization Producer { get; }

        /// <summary>Имя пользователя</summary>        
        [StringLength(255)]
        public string UserName { get; private set; }

        /// <summary>Комментарий</summary>        
        [StringLength(1024)]
        public string Comments { get; private set; }

        /// <summary>Статус заказа</summary>        
        public OrderStatus Status { get; private set; } = OrderStatus.New;

        /// <summary>Состав заказа</summary>
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public virtual IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        /// <summary>
        /// Добавить продукцию в заказ
        /// </summary> 
        public void AddOrderItems(IReadOnlyCollection<OrderItem> orderItems)
        {

            _orderItems.AddRange(orderItems);
        }

        /// <summary>
        /// Установить статус заказа
        /// </summary>        
        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }

    }
}
