using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.OrdersNS
{
    /// <summary>
    /// Запрос с отбором по заказам
    /// </summary>
    public record FilterOrdersRequest : PagedRequest
    {
        public string? OrganisationName { get; set; }

        public string? UserName { get; set; }

        public string? ProductName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
