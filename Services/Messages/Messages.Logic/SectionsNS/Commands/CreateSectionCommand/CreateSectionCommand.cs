using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Commands.CreateSectionCommand
{
    public class CreateSectionCommand : IRequest<long>
    {
        public long? ParentSectionId { get; set; }
        public string Name { get; set; }
    }
}
