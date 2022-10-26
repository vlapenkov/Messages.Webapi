using Rk.Messages.Logic.CommonNS.Dto;

namespace Rk.Messages.Logic.SectionsNS.Dto
{
    public class SectionDto: BaseDto
    {
        public SectionDto() { }
        public SectionDto(long? parentSectionId, long id, string name)
        {
            ParentSectionId = parentSectionId;
            Id = id;
            Name = name;
        }

        public long? ParentSectionId { get; set; }
       
    }
}
