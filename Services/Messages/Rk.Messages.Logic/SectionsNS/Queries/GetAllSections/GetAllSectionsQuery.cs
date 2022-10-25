using System.Collections.Generic;
using MediatR;
using Rk.Messages.Logic.SectionsNS.Dto;

namespace Rk.Messages.Logic.SectionsNS.Queries.GetAllSections
{
    public class GetAllSectionsQuery :IRequest<IReadOnlyCollection<SectionDto>>
    {
        public long? ParentSectionId { get; set; }
    }
}
