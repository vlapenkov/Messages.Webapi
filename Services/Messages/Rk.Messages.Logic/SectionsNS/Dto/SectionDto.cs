using Rk.Messages.Logic.CommonNS.Dto;

namespace Rk.Messages.Logic.SectionsNS.Dto
{
    public record SectionDto
    {
        public long? ParentSectionId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }

    }
}
