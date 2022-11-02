namespace Rk.Messages.Spa.Infrastructure.Dto.SectionsNS
{
    public record SectionDto
    {
        public long? ParentSectionId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
