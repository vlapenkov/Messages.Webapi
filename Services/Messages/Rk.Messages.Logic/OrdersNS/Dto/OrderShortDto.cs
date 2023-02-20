using Rk.Messages.Logic.CommonNS.Dto;

namespace Rk.Messages.Logic.OrdersNS.Dto
{
    /// <summary>
    /// Информация о заказе в списке
    /// </summary>
    public record OrderShortDto : AuditableEntityDto
    {

        public string OrganisationName { get; set; }

        public string ProducerName { get; set; }

        public string UserName { get; set; }

        public string Comments { get; set; }

        public long Quantity { get; set; }

        public decimal Sum { get; set; }

        public string StatusText { get; set; }
    }
}
