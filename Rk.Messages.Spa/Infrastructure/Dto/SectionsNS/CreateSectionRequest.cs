namespace Rk.Messages.Spa.Infrastructure.Dto.SectionsNS
{
    public class CreateSectionRequest
    {
        public long? ParentSectionId { get; set; }

        public string Name { get; set; }
    }
}
