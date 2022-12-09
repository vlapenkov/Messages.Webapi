using Rk.Messages.Logic.CommonNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Dto
{
    /// <summary>
    /// Запрос с отбором
    /// </summary>
    public record FilterOrdersRequest : PagedRequest
    {
        public string OrganisationName { get; set; }

        public long? OrganisationId { get; set; }

        public string ProducerName { get; set; }

        public long? ProducerId { get; set; }

        public string UserName { get; set; }

        public string ProductName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
