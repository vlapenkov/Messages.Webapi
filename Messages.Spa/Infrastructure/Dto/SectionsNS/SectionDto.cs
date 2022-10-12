using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Spa.Infrastructure.Dto.SectionsNS
{
    public class SectionDto
    {
        public long? ParentSectionId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
