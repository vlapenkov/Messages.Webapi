using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.OrdersNS
{
    public record OrderResponse : AuditableEntityDto
    {
        public string OrganisationName { get; set; }

        public string UserName { get; set; }

        public string Comments { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        public decimal Sum => OrderItems.Sum(oi=>oi.Sum);

        public string StatusText { get; set; }
    }
}
