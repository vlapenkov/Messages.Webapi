﻿using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.OrdersNS
{
    /// <summary>
    /// Информация о заказе в списке
    /// </summary>
    public record OrderShortDto : AuditableEntityDto
    {
        public long Id { get; set; }

        public string OrganisationName { get; set; }

        public string UserName { get; set; }

        public string Comments { get; set; }

        public long Quantity { get; set; }

        public decimal Sum { get; set; }
    }
}
