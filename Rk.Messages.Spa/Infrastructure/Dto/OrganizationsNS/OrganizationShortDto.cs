using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.ProductsNS
{
    public record OrganizationShortDto : AuditableEntityDto
    {        
        public string Name { get; set; }

        public string Region { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        /// <summary>Признак организации- производителя</summary>
        public bool IsProducer { get; set; }

        /// <summary>Признак организации- покупателя</summary>
        public bool IsBuyer { get; set; }
    }
}
