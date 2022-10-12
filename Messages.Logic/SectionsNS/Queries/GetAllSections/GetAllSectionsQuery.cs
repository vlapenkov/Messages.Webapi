using MediatR;
using Messages.Logic.SectionsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Queries.GetAllSections
{
    public class GetAllSectionsQuery :IRequest<IReadOnlyCollection<SectionDto>>
    {
        public long? ParentSectionId { get; set; }
    }
}
