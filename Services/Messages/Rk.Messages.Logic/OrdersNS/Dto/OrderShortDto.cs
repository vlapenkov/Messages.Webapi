﻿using Rk.Messages.Logic.CommonNS.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Dto
{
    /// <summary>
    /// Информация о заказе в списке
    /// </summary>
    public record OrderShortDto :AuditableEntityDto
    {
        public long Id { get; set; }

        public string OrganisationName { get; set; }

        public string UserName { get; set; }
        
        public string Comments { get; set; }

        public long Quantity { get; set; }

        public decimal Sum { get; set; }
    }
}
