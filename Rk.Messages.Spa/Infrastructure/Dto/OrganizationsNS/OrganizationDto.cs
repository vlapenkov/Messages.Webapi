using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.OrganizationsNS
{
    public record OrganizationDto : AuditableEntityDto
    {

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Ogrn { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Site { get; set; }

        public string Okved { get; set; }

        public string Okved2 { get; set; }

        public long Status { get; set; }        

    }
}
