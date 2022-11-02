using Rk.Messages.Domain.Entities;
using Rk.Messages.Logic.CommonNS.Dto;

namespace Rk.Messages.Logic.SectionsNS.Dto
{
    public record SectionDto :AuditableEntityDto
    {
        public long? ParentSectionId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }

    }
}
