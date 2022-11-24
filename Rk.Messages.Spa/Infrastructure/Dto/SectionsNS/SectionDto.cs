using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;

namespace Rk.Messages.Spa.Infrastructure.Dto.SectionsNS
{
    public record SectionDto : AuditableEntityDto
    {
        public long? ParentSectionId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public Guid? DocumentId { get; set; }
    }
}
