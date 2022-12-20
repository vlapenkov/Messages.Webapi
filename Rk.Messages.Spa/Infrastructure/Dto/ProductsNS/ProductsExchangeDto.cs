using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    public record ProductsExchangeDto : AuditableEntityDto
    {
        public string ProductExchangeText { get; set; }

        public long ProductsLoaded { get; set; }
    }
}
