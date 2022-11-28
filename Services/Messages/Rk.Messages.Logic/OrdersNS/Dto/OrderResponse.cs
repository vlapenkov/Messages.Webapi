using Rk.Messages.Logic.CommonNS.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Dto
{
    /// <summary>
    /// Информация о заказе
    /// </summary>
    public record OrderResponse :AuditableEntityDto
    {
        public long Id { get; set; }

        /// <summary>Название организации</summary>
        public string OrganisationName  { get; set; }

        /// <summary>Имя пользователя</summary>
        public string UserName { get; set; }

        /// <summary>Комментарий</summary>
        public string Comments { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        public decimal Sum  => OrderItems.Sum(oi => oi.Sum);

        public string StatusText { get; set; }
    }
}
