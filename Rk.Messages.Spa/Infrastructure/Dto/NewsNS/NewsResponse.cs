using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.NewsNS
{
    public record NewsResponse : AuditableEntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? DocumentId { get; set; }
    }
}
