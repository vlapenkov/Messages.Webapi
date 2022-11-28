using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.SectionsNS.Commands.DeleteSection
{
    public class DeleteSectionCommand :IRequest
    {
        public long SectionId { get; set; }
    }
}
