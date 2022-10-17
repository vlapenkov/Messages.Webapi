using Messages.Logic.CommonNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Dto
{
    public class SectionDto: BaseDto
    {
        public SectionDto(long? parentSectionId, long id, string name)
        {
            ParentSectionId = parentSectionId;
            Id = id;
            Name = name;
        }

        public long? ParentSectionId { get; set; }
       
    }
}
